namespace SHWDTech.Platform.Model.IModel
{
    public interface IDomainModel
    {
        /// <summary>
        /// 所属域
        /// </summary>
        ISysDomain Domain { get; set; }
    }
}
