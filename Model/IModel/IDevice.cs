using SHWDTech.Platform.Model.Model;
using System;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 设备信息模型接口
    /// </summary>
    public interface IDevice : ISysDomainModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        DeviceType DeviceType { get; set; }

        /// <summary>
        /// 设备对应的原设备
        /// </summary>
        Device OriginalDevice { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        /// 设备访问密码
        /// </summary>
        string DevicePassword { get; set; }

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        Guid DeviceGuid { get; set; }

        /// <summary>
        /// 设备NODE编码
        /// </summary>
        byte[] DeviceNodeId { get; set; }

        /// <summary>
        /// 设备关联固件集
        /// </summary>
        FirmwareSet FirmwareSet { get; set; }

        /// <summary>
        /// 设备所属项目
        /// </summary>
        Project Project { get; set; }

        /// <summary>
        /// 设备启用时间
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 设备预结束时间
        /// </summary>
        DateTime PreEndTime { get; set; }

        /// <summary>
        /// 设备结束时间
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        DeviceStatus Status { get; set; }

        /// <summary>
        /// 设备关联摄像头
        /// </summary>
        Camera Camera { get; set; }
    }
}