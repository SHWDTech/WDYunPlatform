namespace SHWDTech.Platform.Model.IModel
{
    public interface IPermission : ISysModel, IDomainModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        string PermissionName { get; set; }
    }
}
