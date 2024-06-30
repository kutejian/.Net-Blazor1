using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steven.QuartZDemo.QuartZListener;
//继承ITriggerListener   用于监听与触发器相关的事件  
public class StevenTriggerListener : ITriggerListener
{
    public string Name => "StevenTriggerListener";
    //当触发器完成时调用 trigger：已触发的触发器。context：任务执行上下文。triggerInstructionCode：调度器指令代码。cancellationToken：取消标记。
    //该方法在触发器完成时被调用，你可以在这里执行一些逻辑。
    public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven TriggerComplete 333333333333:{DateTime.Now}");
        await Task.CompletedTask;
    }
    //当触发器被触发时调用 trigger：已触发的触发器。context：任务执行上下文。cancellationToken：取消标记。
    //该方法在触发器被触发时调用，你可以在这里执行一些逻辑。
    public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven TriggerFired 111111111111:{DateTime.Now}");
        await Task.CompletedTask;
    }
    //当触发器错过触发时调用 trigger：已触发的触发器。cancellationToken：取消标记。
    //该方法在触发器错过触发时调用，你可以在这里执行一些逻辑。
    public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven TriggerMisfired 44444444444:{DateTime.Now}");
        await Task.CompletedTask;
    }
    //当决定是否阻止任务执行时调用 trigger：已触发的触发器。context：任务执行上下文。cancellationToken：取消标记。
    //返回 Task<bool>，如果返回 true，则标识任务被取消，如果返回 false，则任务会正常执行。在这里，
    //通过返回 cancellationToken.IsCancellationRequested 来判断是否应该取消任务的执行。
    public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Steven VetoJobExecution 22222222222:{DateTime.Now}");
        //如果直接返回为True，则标识任务取消
        //return await Task.FromResult(true);
        return await Task.FromResult(cancellationToken.IsCancellationRequested);
    }
}
