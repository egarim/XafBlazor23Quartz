﻿using Quartz;
using Quartz.Spi;
using System;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace XafBlazor23Quartz.Blazor.Server.Quartz.Quartz
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) { }
    }
}
