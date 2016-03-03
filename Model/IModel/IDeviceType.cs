using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 设备类型接口
    /// </summary>
    public interface IDeviceType : ISysModel
    {
        /// <summary>
        /// 设备应用领域
        /// </summary>
        SysDictionary Field { get; set; }

        /// <summary>
        /// 设备应用子领域
        /// </summary>
        SysDictionary SubField { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        string CustomerInfo { get; set; }

        /// <summary>
        /// 设备版本号
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 设备发布时间
        /// </summary>
        DateTime ReleaseDateTime { get; set; }

        /// <summary>
        /// 设备类型编码
        /// </summary>
        string DeviceTypeCode { get; set; }
    }
}
