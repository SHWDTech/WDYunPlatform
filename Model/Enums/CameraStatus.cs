namespace SHWDTech.Platform.Model.Enums
{
    public enum CameraStatus : byte
    {
        /// <summary>
        /// 未关联设备
        /// </summary>
        NotRelated = 0x00,

        /// <summary>
        /// 未初始化
        /// </summary>
        NotInitialized = 0x01,

        /// <summary>
        /// 已经启用
        /// </summary>
        Enabled = 0x02,

        /// <summary>
        /// 已经停用
        /// </summary>
        Disabled = 0x03
    }
}