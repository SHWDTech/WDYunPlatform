namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 净化器类型
    /// </summary>
    public enum ClearnerType : byte
    {
        /// <summary>
        /// 静电式
        /// </summary>
        Electrostatic = 0x00,

        /// <summary>
        /// 过滤式
        /// </summary>
        Filter = 0x01,

        /// <summary>
        /// 负离子
        /// </summary>
        Anion = 0x02,

        /// <summary>
        /// 光电式
        /// </summary>
        Photoelectric = 0x03
    }
}
