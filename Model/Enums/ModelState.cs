namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 数据对象模型状态
    /// </summary>
    public enum ModelState : byte
    {
        /// <summary>
        /// 新增
        /// </summary>
        Added = 0,

        /// <summary>
        /// 未改变
        /// </summary>
        UnChanged = 1,

        /// <summary>
        /// 从数据库新增
        /// </summary>
        AddedFromDb = 2
    }
}