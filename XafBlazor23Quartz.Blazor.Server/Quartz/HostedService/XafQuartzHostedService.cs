﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XafBlazor23Quartz.Blazor.Server.Quartz.Jobs;
using XafBlazor23Quartz.Blazor.Server.Quartz.ObjectSpaceService;
using XafBlazorQuartzHostedService.Module.Blazor.Quartz;
using XafBlazorQuartzHostedService.Module.BusinessObjects;

namespace XafBlazorQuartzHostedService.Module.Blazor.Quartz
{
    public class XafQuartzHostedService : IHostedService
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IQuartzObjectSpaceService _objectSpaceService;


        public XafQuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
           IQuartzObjectSpaceService objectSpaceService)
        {
            _schedulerFactory = schedulerFactory;
            _objectSpaceService = objectSpaceService;
            _jobFactory = jobFactory;

        }
        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            IObjectSpace Os;
            IEnumerable<ScheduleBase> Schedules = null;
            try
            {
                Os = _objectSpaceService.GetObjectSpace();

                Schedules = Os.CreateCollection(typeof(ScheduleBase)).Cast<ScheduleBase>();
                //HACK get the count to evaluate the property
                var Count = Schedules.Count();
            }
            catch (Exception ex) when (ex is DevExpress.Xpo.DB.Exceptions.SchemaCorrectionNeededException || ex is DevExpress.Xpo.DB.Exceptions.UnableToOpenDatabaseException)
            {

                return;
            }


            foreach (var item in Schedules)
            {
                var jobSchedule = new JobSchedule(
                    jobType: typeof(XafJob),
                    cronExpression: item.Expression);


                IDictionary<string, object> map = new Dictionary<string, object>()
                {
                    //hack to avoid threading problems its better to pass the connection string and then create an object space provider inside of the job
                    {"Oid",item.Oid },
                    {"cnx",_objectSpaceService.GetConnectionString() },

                };
                var DataMap = new JobDataMap(map);

                var job = CreateJob(jobSchedule, DataMap);
                var trigger = CreateTrigger(jobSchedule);



                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

      

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule schedule, JobDataMap DataMap)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .SetJobData(DataMap)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}