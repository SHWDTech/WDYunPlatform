namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 运行时间类型
    /// </summary>
    public enum RunningTimeType : byte
    {
        /// <summary>
        /// 净化器
        /// </summary>
        Cleaner = 0,

        /// <summary>
        /// 风机
        /// </summary>
        Fan = 1,

        /// <summary>
        /// 设备
        /// </summary>
        Device = 2,

        /// <summary>
        /// 浓度探头
        /// </summary>
        Lampbalck = 3
    }
}
