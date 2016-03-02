namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 数据类型模型接口
    /// </summary>
    public interface IDataModel : IModel
    {
        /// <summary>
        /// 所属域
        /// </summary>
        IDomain Domain { get; set; }
    }
}
