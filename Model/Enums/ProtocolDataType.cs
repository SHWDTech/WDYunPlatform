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
        /// 字节数组
        /// </summary>
        public const string Bytes = "Bytes";

        /// <summary>
        /// 纯数字密码
        /// </summary>
        public const string NumPassword = "NumPassword";

        /// <summary>
        /// 功能码
        /// </summary>
        public const string FunctionCode = "FunctionCode";

        /// <summary>
        /// ASC值表示的时间
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string ASCTime = "ASCTime";

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
        // ReSharper disable once InconsistentNaming
        public const string CRC = "CRC";

        /// <summary>
        /// LRC校验码
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string LRC = "LRC";

        /// <summary>
        /// 数据有效性标志位
        /// </summary>
        public const string DataValidFlag = "DataValidFlag";

        /// <summary>
        /// 四个字节存储的无符号整型
        /// </summary>
        public const string FourBytesToUInt32 = "FourBytesToUInt32";

        /// <summary>
        /// 两个字节存储的Double类型，精度两位小数，一个字节存储整数部分，一个字节存储小数部分
        /// </summary>
        public const string TwoBytesToDoubleSeparate = "TwoBytesToDoubleSeparate";

        /// <summary>
        /// 两个字节存储的无符号短整型
        /// </summary>
        public const string TwoBytesToUShort = "TwoBytesToUShort";

        /// <summary>
        /// 两个字节存储的无符号短整型
        /// </summary>
        public const string ThreeBytesToUShort = "ThreeBytesToUShort";

        /// <summary>
        /// 两个字节存储的Double类型，精度一位小数。
        /// </summary>
        public const string TwoBytesToDoubleMerge = "TwoBytesToDoubleMerge";

        /// <summary>
        /// 四个字节存储两个无符号短整型。
        /// </summary>
        public const string FourBytesToTwoUShortSeparate = "FourBytesToTwoUShortSeparate";

        /// <summary>
        /// 数据来源
        /// </summary>
        public const string DataSource = "DataSource";

        /// <summary>
        /// 保留数据段
        /// </summary>
        public const string Reserved = "Reserved";

        /// <summary>
        /// 单个字节存储的布尔值
        /// </summary>
        public const string ByteToBoolean = "ByteToBoolean";

        /// <summary>
        /// 无
        /// </summary>
        public const string None = "None";
    }
}
