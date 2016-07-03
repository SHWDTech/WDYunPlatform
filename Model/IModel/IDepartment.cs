namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 部门模型接口
    /// </summary>
    public interface IDepartment : ISysDomainModel
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 部门备注
        /// </summary>
        string Comment { get; set; }
    }
}
