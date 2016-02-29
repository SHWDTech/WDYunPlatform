using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IPhoto
    {
        /// <summary>
        /// 照片所属域
        /// </summary>
        SysDomain PhotoDomain { get; set; }

        /// <summary>
        /// 照片所属设备
        /// </summary>
        Device PhtotDevice { get; set; }

        /// <summary>
        /// 照片附加信息
        /// </summary>
        string PhotoTag { get; set; }

        /// <summary>
        /// 照片地址
        /// </summary>
        string PhotoUrl { get; set; }

        /// <summary>
        /// 照片类型
        /// </summary>
        int PhotoType { get; set; }
    }
}
