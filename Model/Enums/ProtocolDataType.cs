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
        public const string ClassicNodeId = "ClassicNodeId";

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
    }
}
