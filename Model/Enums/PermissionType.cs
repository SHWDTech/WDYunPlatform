namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType : byte
    {
        /// <summary>
        /// 控制器
        /// </summary>
        Controller = 0,

        /// <summary>
        /// 控制器方法
        /// </summary>
        Action = 1,

        /// <summary>
        /// 操作
        /// </summary>
        Operate = 2,

        /// <summary>
        /// 按钮
        /// </summary>
        Button = 3
    }
}
