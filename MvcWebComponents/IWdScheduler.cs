using System;
using System.Linq.Expressions;

namespace MvcWebComponents
{
    /// <summary>
    /// 计划任务接口
    /// </summary>
    public interface IWdScheduler
    {
        /// <summary>
        /// 执行间隔时间，单位毫秒
        /// </summary>
        long Interval { get; set; }

        /// <summary>
        /// 计划任务类型
        /// </summary>
        SchedulerType SchedulerType { get; }

        /// <summary>
        /// 计划任务状态
        /// </summary>
        SchedulerState SchedulerState { get; }

        /// <summary>
        /// 最后一次执行时间
        /// </summary>
        DateTime LastExecuteTime { get; }

        /// <summary>
        /// 计划任务开始时间
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 计划任务已经执行次数
        /// </summary>
        int ExecuteTimes { get; set; }

        /// <summary>
        /// 计划任务最后一次执行结果
        /// </summary>
        ExecuteResult ExecuteResult { get; }

        /// <summary>
        /// 计划任务结束条件
        /// </summary>
        Expression<Func<bool>> StopCondition { get; set; }

        /// <summary>
        /// 开始计划任务
        /// </summary>
        void Start();

        /// <summary>
        /// 结束计划任务
        /// </summary>
        void Stop();

        /// <summary>
        /// 暂停计划任务
        /// </summary>
        void Suspend();

        /// <summary>
        /// 尝试执行计划任务
        /// </summary>
        void TryExecute(object state);
    }
}
