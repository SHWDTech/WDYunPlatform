namespace SHWDTech.Platform.Model.IModel
{
    public interface IDataModel : IModel
    {
        /// <summary>
        /// 所属域
        /// </summary>
        ISysDomain Domain { get; set; }
    }
}
