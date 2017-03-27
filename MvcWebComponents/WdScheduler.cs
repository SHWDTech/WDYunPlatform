using System;
using System.Linq.Expressions;
using System.Threading;

namespace MvcWebComponents
{
    public class WdScheduler : IWdScheduler
    {
        private Timer _timer;

        private bool _suspended;

        private int _executedTimes;

        public long Interval { get; set; }

        public SchedulerType SchedulerType { get; set; }

        public SchedulerState SchedulerState { get; private set; }

        public DateTime LastExecuteTime { get; set; }

        public DateTime StartTime { get; set; }

        public int ExecuteTimes { get; set; }

        public ExecuteResult ExecuteResult { get; private set; }

        public Expression<Func<bool>> StopCondition { get; set; }

        public WdScheduler()
        {
            StartTime = LastExecuteTime = DateTime.MinValue;
        }

        public WdScheduler(SchedulerType type) : this()
        {
            SchedulerType = type;
        }

        public WdScheduler(Expression<Func<bool>> condition) : this()
        {
            StopCondition = condition;
        }

        public WdScheduler(SchedulerType type, Expression<Func<bool>> condition) : this()
        {
            SchedulerType = type;
            StopCondition = condition;
        }

        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer(TryExecute, null, 0, Interval);
            }

            _suspended = false;
            SchedulerState = SchedulerState.Running;
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        public void Suspend()
        {
            _suspended = true;
            SchedulerState = SchedulerState.Suspend;
        }

        public void TryExecute(object state)
        {
            if (_suspended) return;
            try
            {
                OnExecuting?.Invoke();
                _executedTimes++;
                SchedulerState = SchedulerState.Finished;
            }
            catch (Exception ex)
            {
                ExecuteResult = new ExecuteResult(ex);
                SchedulerState = SchedulerState.Filed;
            }

            Executed();
        }

        private void Executed()
        {
            AfterExecuting?.Invoke();
            if (SchedulerType == SchedulerType.ExecuteOnce
                || (SchedulerType == SchedulerType.Interval && ExecuteTimes != 0 && ExecuteTimes <= _executedTimes)
                || (SchedulerType == SchedulerType.StopOnCondition && StopCondition != null && StopCondition.Compile().Invoke()))
            {
                _timer.Dispose();
            }
        }

        /// <summary>
        /// 计划任务执行事件
        /// </summary>
        public event OnExecuting OnExecuting;

        /// <summary>
        /// 
        /// </summary>
        public event AfterExecuting AfterExecuting;
    }
}
