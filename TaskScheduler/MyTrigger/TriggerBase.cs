using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerQuartz.MyTrigger
{
    public static class TriggerBase
    {
        //一天刷新一次
        public static ITrigger OnceADayCreateTrigger(IJobDetail job)
        {
            return TriggerBuilder.Create()
                .StartNow()
                .WithPriority(10)
                .ForJob(job)
                .WithCronSchedule("* * * * * ? *")
                //.WithCronSchedule("0 0 0 * * ?")
                .Build();
        }
    }
}
