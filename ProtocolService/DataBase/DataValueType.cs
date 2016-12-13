namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 数据值类型
    /// </summary>
    public enum DataValueType : byte
    {
        /// <summary>
        /// 浮点数
        /// </summary>
        Double = 0x00,

        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean = 0x01,

        /// <summary>
        /// 整型术
        /// </summary>
        Integer = 0x02
    }
}
