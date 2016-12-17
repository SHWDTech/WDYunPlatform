namespace SHWDTech.Platform.ProtocolService
{
    /// <summary>
    /// 服务宿主运行状态
    /// </summary>
    public enum ServiceHostStatus : byte
    {
        /// <summary>
        /// 服务停止运行
        /// </summary>
        Stopped = 0x00,

        /// <summary>
        /// 服务运行中
        /// </summary>
        Running = 0x01
    }
}
