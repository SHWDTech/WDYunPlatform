namespace SHWDTech.Platform.Model.IModel
{
    public interface ISysDomainModel : ISysModel
    {
        /// <summary>
        /// 所属域
        /// </summary>
        ISysDomain Domain { get; set; }
    }
}