namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 协议数据段组合方式
    /// </summary>
    public enum DataOrderType : byte
    {
        /// <summary>
        /// 固定顺序
        /// </summary>
        Order = 0x00,

        /// <summary>
        /// 自由组合
        /// </summary>
        Random = 0x01
    }
}
