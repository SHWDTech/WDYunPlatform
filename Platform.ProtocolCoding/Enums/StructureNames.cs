namespace SHWDTech.Platform.ProtocolCoding.Enums
{
    /// <summary>
    /// 协议结构名称
    /// </summary>
    public static class StructureNames
    {
        /// <summary>
        /// 协议头
        /// </summary>
        public const string Head = "Head";

        /// <summary>
        /// 协议尾
        /// </summary>
        public const string Tail = "Tail";

        /// <summary>
        /// 数据结构
        /// </summary>
        public const string Data = "Data";

        /// <summary>
        /// 命令类型
        /// </summary>
        public const string CmdType = "CmdType";

        /// <summary>
        /// 命令符
        /// </summary>
        public const string CmdByte = "CmdByte";

        /// <summary>
        /// 密码
        /// </summary>
        public const string Password = "Password";

        /// <summary>
        /// 设备身份ID号
        /// </summary>
        public const string NodeId = "NodeId";

        /// <summary>
        /// 描述
        /// </summary>
        public const string Description = "Description";

        /// <summary>
        /// 源地址
        /// </summary>
        public const string SourceAddr = "SourceAddr";

        /// <summary>
        /// 目标地址
        /// </summary>
        public const string DestinationAddr = "DestinationAddr";

        /// <summary>
        /// 有效数据长度
        /// </summary>
        public const string PayloadLength = "PayhloadLength";

        /// <summary>
        /// CRC校验码
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string CRCValue = "CRCValue";
    }
}