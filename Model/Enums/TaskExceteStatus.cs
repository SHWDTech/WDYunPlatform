namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 任务执行状态
    /// </summary>
    public enum TaskExceteStatus : byte
    {
        /// <summary>
        /// 等待发送
        /// </summary>
        WaitForSend,

        /// <summary>
        /// 指令已经发送
        /// </summary>
        Sended,

        /// <summary>
        /// 已说到回应
        /// </summary>
        Responsed
    }
}
