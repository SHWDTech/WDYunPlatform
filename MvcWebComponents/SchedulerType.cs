namespace MvcWebComponents
{
    public enum SchedulerType : byte
    {
        /// <summary>
        /// 执行一次
        /// </summary>
        ExecuteOnce = 0x01,

        /// <summary>
        /// 重复执行
        /// </summary>
        Interval = 0x02,

        /// <summary>
        /// 满足特性情况时结束
        /// </summary>
        StopOnCondition = 0x03
    }
}
