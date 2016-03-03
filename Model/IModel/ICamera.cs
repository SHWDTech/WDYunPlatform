namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 摄像头模型接口
    /// </summary>
    public interface ICamera : ISysDomainModel
    {
        /// <summary>
        /// 摄像头外部ID
        /// </summary>
        string CameraOutId { get; set; }

        /// <summary>
        /// 摄像头登录名
        /// </summary>
        string AccessName { get; set; }

        /// <summary>
        /// 摄像头登录密码
        /// </summary>
        string AccessPassword { get; set; }

        /// <summary>
        /// 摄像头登录地址
        /// </summary>
        string AccessUrl { get; set; }

        /// <summary>
        /// 摄像头登陆端口
        /// </summary>
        int AccessPort { get; set; }

        /// <summary>
        /// 摄像头登录类型
        /// </summary>
        int AccessType { get; set; }

        /// <summary>
        /// 摄像头所属公司
        /// </summary>
        string Compnany { get; set; }

        /// <summary>
        /// 摄像头附加信息
        /// </summary>
        string ExtraInformation { get; set; }
    }
}