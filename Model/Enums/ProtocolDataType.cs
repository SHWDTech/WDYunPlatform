namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 指令数据类型
    /// </summary>
    public static class ProtocolDataType
    {
        /// <summary>
        /// 字节数字
        /// </summary>
        public const string SingleByte = "SingleByte";

        /// <summary>
        /// 纯数字密码
        /// </summary>
        public const string NumPassword = "NumPassword";

        /// <summary>
        /// 经典协议设备身份标识
        /// </summary>
        public const string NodeId = "NodeId";

        /// <summary>
        /// 暂不解码
        /// </summary>
        public const string Description = "Description";

        /// <summary>
        /// 源地址
        /// </summary>
        public const string SourceAddr = "SourceAddr";

        /// <summary>
        /// 目标地址
        /// </summary>
        public const string Destination = "Destination";

        /// <summary>
        /// 数据段长度
        /// </summary>
        public const string DataLength = "DataLength";

        /// <summary>
        /// 数据段
        /// </summary>
        public const string Data = "Data";

        /// <summary>
        /// CRC校验码
        /// </summary>
        public const string Crc = "Crc";

        /// <summary>
        /// 数据有效性标志位
        /// </summary>
        public const string DataValidFlag = "DataValidFlag";

        /// <summary>
        /// 颗粒物数值
        /// </summary>
        public const string ParticulateMatter = "ParticulateMatter";

        /// <summary>
        /// 噪音数据
        /// </summary>
        public const string Noise = "Noise";

        /// <summary>
        /// 风向值
        /// </summary>
        public const string WindDirction = "WindDirction";

        /// <summary>
        /// 风速值
        /// </summary>
        public const string WindSpeed = "WindSpeed";

        /// <summary>
        /// 温湿度
        /// </summary>
        public const string TemperatureAndHumidity = "TemperatureAndHumidity";

        /// <summary>
        /// 挥发性有机物数值
        /// </summary>
        public const string VolatileOrganicCompounds = "VolatileOrganicCompounds";
    }
}
