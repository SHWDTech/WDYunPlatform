namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 权限模型接口
    /// </summary>
    public interface IPermission : ISysDomainModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        string PermissionName { get; set; }
    }
}
