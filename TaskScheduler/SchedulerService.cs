using Quartz;
using Steven.QuartZDemo.QuartZListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerQuartz.MyJobS;
using TaskSchedulerQuartz.MySchedulerConfig;
using TaskSchedulerQuartz.MyTrigger;

namespace TaskSchedulerQuartz
{
    public class SchedulerService
    {
        private readonly IScheduler _scheduler = SchedulerConfig.GetScheduler();

        public void RefreshIeaderboardRegularlyJobScheduleJob()
        {


            IJobDetail job = JobBuilder.Create<RefreshIeaderboardRegularlyJob>()
                .WithIdentity("RefreshIeaderboardRegularlyJob", "ProductGroup")
                .Build();

            ITrigger trigger = TriggerBase.OnceADayCreateTrigger(job);

            //下面3个都是监听器 也就是Aop 在执行之前做什么

            //触发器监听器
            _scheduler.ListenerManager.AddTriggerListener(new StevenTriggerListener());

            //调度器监听器  需要实现的方法有很多
            //_scheduler.ListenerManager.AddSchedulerListener(new StevenSchedulerListener());

            //任务监听器
            _scheduler.ListenerManager.AddJobListener(new StevenJobListener());

            _scheduler.ScheduleJob(job, trigger);

            //确定触发器 触发
            //_scheduler.Start();
        }
    }
}
