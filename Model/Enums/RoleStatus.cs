namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 角色状态
    /// </summary>
    public enum RoleStatus
    {
        /// <summary>
        /// 已启用
        /// </summary>
        Enabled = 0x01,

        /// <summary>
        /// 已禁用
        /// </summary>
        Disabled = 0x02,

        /// <summary>
        /// 已暂停
        /// </summary>
        Stopped = 0x03,

        /// <summary>
        /// 已锁定
        /// </summary>
        Locked = 0x04
    }
}