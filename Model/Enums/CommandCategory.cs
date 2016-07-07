namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 指令类型
    /// </summary>
    public static class CommandCategory 
    {
        /// <summary>
        /// 身份验证指令
        /// </summary>
        public const string Authentication = "Authentication";

        /// <summary>
        /// 心跳包指令
        /// </summary>
        public const string HeartBeat = "HeartBeat";

        /// <summary>
        /// 定时自动上报
        /// </summary>
        public const string TimingAutoReport = "TimingAutoReport";

        /// <summary>
        /// 设备控制
        /// </summary>
        public const string DeviceControl = "DeviceControl";

        /// <summary>
        /// 时间校准
        /// </summary>
        public const string SystemCommunication = "SystemCommunication";

        /// <summary>
        /// 设备设置
        /// </summary>
        public const string ModuleConfig = "ModuleConfig";
    }
}
