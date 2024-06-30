using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerQuartz.MySchedulerConfig
{
    public static class SchedulerConfig
    {
        public static IScheduler GetScheduler()
        {
            NameValueCollection pars = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "MyScheduler",
                ["quartz.threadPool.threadCount"] = "20",
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                ["quartz.jobStore.useProperties"] = "true",
                ["quartz.jobStore.dataSource"] = "QuartzDb",
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                ["quartz.dataSource.QuartzDb.connectionString"] = @"TrustServerCertificate=true;server=.;database=QuartzDb;uid=sa;pwd=123",
                ["quartz.dataSource.QuartzDb.provider"] = "SqlServer",
                ["quartz.serializer.type"] = "json",
                ["quartz.scheduler.instanceId"] = "AUTO",
                ["quartz.jobStore.clustered"] = "false"
            };

            StdSchedulerFactory factory = new StdSchedulerFactory(pars);
            return factory.GetScheduler().Result;
        }
    }
}
