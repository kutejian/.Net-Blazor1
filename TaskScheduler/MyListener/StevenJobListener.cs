using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steven.QuartZDemo.QuartZListener;

////任务监听器
public class StevenJobListener : IJobListener
{

    //获取监听器的名称。在这里，名称被设置为 "StevenJobListener"。
    public string Name => "StevenJobListener";
    //在 IJob 实例的执行被否决时调用。如果你在 JobToBeExecuted 方法中返回了 true，那么这个方法将被调用。否决执行是指在任务执行前取消了任务的执行。
    public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven JobExecutionVetoed 333333333333:{DateTime.Now}");
        await Task.CompletedTask;
    }
    //在 IJob 实例即将被执行时调用。这是任务执行前的回调方法。
    public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven JobToBeExecuted 11111111111:{DateTime.Now}");
        await Task.CompletedTask;
    }
    //在 IJob 实例执行完成后调用。这是任务执行后的回调方法。如果任务执行过程中发生异常，jobException 参数将包含异常信息。
    public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven JobWasExecuted 22222222222:{DateTime.Now}");
        await Task.CompletedTask;
    }
}
