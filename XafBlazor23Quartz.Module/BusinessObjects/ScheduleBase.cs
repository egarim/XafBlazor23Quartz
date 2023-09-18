using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafBlazorQuartzHostedService.Module.BusinessObjects
{
    [DefaultClassOptions()]
    [ModelDefault("IsCloneable", "true")]
    public class ScheduleBase : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ScheduleBase(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Monday = true;
            this.Tuesday = true;
            this.Wednesday = true;
            this.Thursday = true;
            this.Friday = true;
            //this.Saturday = true;
            //this.Sunday = true;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        
        bool everyMonth;
        int month;
        bool everyDay;
        int day;
        bool everyHour;
        bool everyMinute;
        bool everySecond;
        string expressionDescription;
        private bool manualExpression;
        #region DaysOfTheWeek


        bool everyDayOfTheWeek;
        public bool EveryDayOfTheWeek
        {
            get => everyDayOfTheWeek;
            set => SetPropertyValue(nameof(EveryDayOfTheWeek), ref everyDayOfTheWeek, value);
        }

        private bool monday;

        public bool Monday
        {
            get
            {
                return monday;
            }
            set
            {
                SetPropertyValue(nameof(Monday), ref monday, value);
            }
        }

        private bool tuesday;

        public bool Tuesday
        {
            get
            {
                return tuesday;
            }
            set
            {
                SetPropertyValue(nameof(Tuesday), ref tuesday, value);
            }
        }

        private bool wednesday;

        public bool Wednesday
        {
            get
            {
                return wednesday;
            }
            set
            {
                SetPropertyValue(nameof(Wednesday), ref wednesday, value);
            }
        }

        private bool thursday;

        public bool Thursday
        {
            get
            {
                return thursday;
            }
            set
            {
                SetPropertyValue(nameof(Thursday), ref thursday, value);
            }
        }

        private bool friday;

        public bool Friday
        {
            get
            {
                return friday;
            }
            set
            {
                SetPropertyValue(nameof(Friday), ref friday, value);
            }
        }

        private bool saturday;

        public bool Saturday
        {
            get
            {
                return saturday;
            }
            set
            {
                SetPropertyValue(nameof(Saturday), ref saturday, value);
            }
        }

        private bool sunday;

        public bool Sunday
        {
            get
            {
                return sunday;
            }
            set
            {
                SetPropertyValue(nameof(Sunday), ref sunday, value);
            }
        }


        #endregion

        private string expression;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Expression
        {
            get
            {
                return expression;
            }
            set
            {
                SetPropertyValue(nameof(Expression), ref expression, value);
            }
        }



        public bool EveryHour
        {
            get => everyHour;
            set => SetPropertyValue(nameof(EveryHour), ref everyHour, value);
        }

        private int hour;

        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                SetPropertyValue(nameof(Hour), ref hour, value);
            }
        }


        public bool EveryMinute
        {
            get => everyMinute;
            set => SetPropertyValue(nameof(EveryMinute), ref everyMinute, value);
        }

        private int minute;

        public int Minute
        {
            get
            {
                return minute;
            }
            set
            {
                SetPropertyValue(nameof(Minute), ref minute, value);
            }
        }
        public bool EverySecond
        {
            get => everySecond;
            set => SetPropertyValue(nameof(EverySecond), ref everySecond, value);
        }

        int second;

        public int Second
        {
            get => second;
            set => SetPropertyValue(nameof(Second), ref second, value);
        }

        public bool EveryDay
        {
            get => everyDay;
            set => SetPropertyValue(nameof(EveryDay), ref everyDay, value);
        }

        public int Day
        {
            get => day;
            set => SetPropertyValue(nameof(Day), ref day, value);
        }

        
        public bool EveryMonth
        {
            get => everyMonth;
            set => SetPropertyValue(nameof(EveryMonth), ref everyMonth, value);
        }

        public int Month
        {
            get => month;
            set => SetPropertyValue(nameof(Month), ref month, value);
        }

        public bool ManualExpression
        {
            get => manualExpression;
            set => SetPropertyValue(nameof(ManualExpression), ref manualExpression, value);
        }

        protected override void OnSaving()
        {
            if (!ManualExpression)
            {
                BuildExpression();
            }

            base.OnSaving();
        }

        protected virtual StringBuilder BuildSeconds(StringBuilder Builder)
        {
            if (this.EverySecond)
            {
                Builder.Append("*");
            }
            else
            {
                if (!(this.Second >= 0 && this.Second <= 59))
                    throw new ArgumentException("The hour parameter should greater or equal than 0 and less or equal to 59", nameof(Second));
                Builder.Append(this.Hour);

            }
            Builder.Append(" ");
            return Builder;
        }

        protected virtual StringBuilder BuildMinutes(StringBuilder Builder)
        {
            if (this.EveryMinute)
            {
                Builder.Append("*");
            }
            else
            {
                if (!(this.Minute >= 0 && this.Minute <= 59))
                    throw new ArgumentException($"The {nameof(this.Minute).ToLower()}  parameter should greater or equal than 0 and less or equal to 59", nameof(Minute));
                Builder.Append(this.Hour);

            }
            Builder.Append(" ");
            return Builder;
        }
        protected virtual StringBuilder BuildHours(StringBuilder Builder)
        {
            if (this.EveryHour)
            {
                Builder.Append("*");
            }
            else
            {
                if (!(this.Hour >= 0 && this.Hour <= 59))
                    throw new ArgumentException($"The {nameof(this.Hour).ToLower()}  parameter should greater or equal than 0 and less or equal to 59", nameof(Hour));
                Builder.Append(this.Hour);

            }
            Builder.Append(" ");
            return Builder;
        }
        protected virtual StringBuilder BuildMonths(StringBuilder Builder)
        {
            if (this.EveryMonth)
            {
                Builder.Append("*");
            }
            else
            {
                if (!(this.Month >= 1 && this.Month <= 12))
                    throw new ArgumentException($"The {nameof(this.Month).ToLower()}  parameter should greater or equal than 1 and less or equal to 12", nameof(Month));
                Builder.Append(this.Hour);

            }
            Builder.Append(" ");
            return Builder;
        }
        protected virtual StringBuilder BuildDayOfTheWeek(StringBuilder Builder)
        {
            if (this.everyDayOfTheWeek)
            {
                Builder.Append("*");
            }
            else
            {
                if (Monday)
                {
                    Builder.Append("MON");
                    Builder.Append(",");
                }
                if (Tuesday)
                {
                    Builder.Append("TUE");
                    Builder.Append(",");
                }
                if (Wednesday)
                {
                    Builder.Append("WED");
                    Builder.Append(",");
                }
                if (Thursday)
                {
                    Builder.Append("THU");
                    Builder.Append(",");
                }
                if (Friday)
                {
                    Builder.Append("FRI");
                    Builder.Append(",");
                }
                if (Saturday)
                {
                    Builder.Append("SAT");
                    Builder.Append(",");
                }
                if (Sunday)
                {
                    Builder.Append("SUN");
                    Builder.Append(",");
                }

            }
            Builder.Append(" ");
            return Builder;
        }
        protected virtual StringBuilder BuildDayOfTheMonth(StringBuilder Builder)
        {

            if (this.EveryDay)
            {
                Builder.Append("*");
            }
            else
            {
                if (!(this.Day>= 1 && this.Day <= 31))
                    throw new ArgumentException("The day parameter should greater or equal than 1 and less or equal to 31", nameof(Day));
                Builder.Append(this.Day);
                
            }
            Builder.Append(" ");
            return Builder;
        }
      
        private void BuildExpression()
        {


            //HACK https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html


            //Cron Expressions
            //Cron - Expressions are used to configure instances of CronTrigger.Cron - Expressions are strings that are actually made up of seven sub-expressions, that describe individual details of the schedule.These sub-expression are separated with white-space, and represent:

            //Seconds
            //Minutes
            //Hours
            //Day - of - Month
            //Month
            //Day - of - Week
            //Year(optional field)

            this.LastUpdate = DateTime.UtcNow.Ticks;
            StringBuilder Builder = new StringBuilder();
            Builder = this.BuildSeconds(Builder);
            Builder = this.BuildMinutes(Builder);
            Builder = this.BuildHours(Builder);
            Builder = this.BuildDayOfTheMonth(Builder);
            Builder = this.BuildMonths(Builder);
            Builder = this.BuildDayOfTheWeek(Builder);
            
           
            var BaseExpression = Builder.ToString().TrimEnd(',');
            BaseExpression = BaseExpression + " *";//Year
            this.Expression = BaseExpression;
        }

        public string GetDaysExpression()
        {
            StringBuilder Builder = new StringBuilder();
            if (Monday)
            {
                Builder.Append("MON");
                Builder.Append(",");
            }
            if (Tuesday)
            {
                Builder.Append("TUE");
                Builder.Append(",");
            }
            if (Wednesday)
            {
                Builder.Append("WED");
                Builder.Append(",");
            }
            if (Thursday)
            {
                Builder.Append("THU");
                Builder.Append(",");
            }
            if (Friday)
            {
                Builder.Append("FRI");
                Builder.Append(",");
            }
            if (Saturday)
            {
                Builder.Append("SAT");
                Builder.Append(",");
            }
            if (Sunday)
            {
                Builder.Append("SUN");
                Builder.Append(",");
            }
            return Builder.ToString().TrimEnd(',');
        }

        private System.DateTime startDate;

        public System.DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                SetPropertyValue(nameof(StartDate), ref startDate, value);
            }
        }

        private System.DateTime endDate;

        public System.DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                SetPropertyValue(nameof(EndDate), ref endDate, value);
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == nameof(Expression) && newValue != null)
            {
                this.ExpressionDescription = CronExpressionDescriptor.ExpressionDescriptor.GetDescription(newValue.ToString());
            }

        }

        //[ValueConverter(typeof(TypeToStringConverter))]
        //[TypeConverter(typeof(TypeToJobValueConverter))]
        //[Size(SizeAttribute.Unlimited)]
        //public Type Job
        //{
        //    get { return GetPropertyValue<Type>("Job"); }
        //    set { SetPropertyValue<Type>("Job", value); }
        //}

        private long lastUpdate;


        [Size(SizeAttribute.Unlimited)]
        public string ExpressionDescription
        {
            get => expressionDescription;
            set => SetPropertyValue(nameof(ExpressionDescription), ref expressionDescription, value);
        }


        [Browsable(false)]
        public long LastUpdate
        {
            get => lastUpdate;
            set => SetPropertyValue(nameof(LastUpdate), ref lastUpdate, value);
        }
        [Association("ScheduleBase-ScheduleExecutionDetails")]
        public XPCollection<ScheduleExecutionDetail> ScheduleExecutionDetails
        {
            get
            {
                return GetCollection<ScheduleExecutionDetail>(nameof(ScheduleExecutionDetails));
            }
        }

    }
}