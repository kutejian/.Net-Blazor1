using LearnBlazorRepository.Repository.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerQuartz.MyJobS
{
    public class RefreshIeaderboardRegularlyJob : IJob
    {
       
        public RefreshIeaderboardRegularlyJob() 
        {

            Console.WriteLine("Myjob 构造函数");
        }
        public async Task Execute(IJobExecutionContext context)
        {
          
            Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}  Myjob excute:" + DateTime.Now);
            await Task.CompletedTask;
        }
    }
}
