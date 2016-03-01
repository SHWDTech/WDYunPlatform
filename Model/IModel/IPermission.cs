namespace SHWDTech.Platform.Model.IModel
{
    public interface IPermission : ISysDomainModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        string PermissionName { get; set; }
    }
}
