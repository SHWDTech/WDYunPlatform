namespace MvcWebComponents
{
    /// <summary>
    /// 计划任务状态
    /// </summary>
    public enum SchedulerState : byte
    {
        /// <summary>
        /// 未注册
        /// </summary>
        UnRegistered = 0x00,

        /// <summary>
        /// 正在执行
        /// </summary>
        Running = 0x01,

        /// <summary>
        /// 暂停
        /// </summary>
        Suspend = 0x02,

        /// <summary>
        /// 执行完毕
        /// </summary>
        Finished = 0x03,

        /// <summary>
        /// 执行失败
        /// </summary>
        Filed = 0x04,
    }
}
