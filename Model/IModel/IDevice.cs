using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IDevice : ISysDomainModel
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        string DeviceCode { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        /// 设备NODE编码
        /// </summary>
        byte[] DeviceNodeId { get; set; }

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
        int Status { get; set; }
        /// <summary>
        /// 设备关联摄像头
        /// </summary>
        Camera Camera { get; set; }
    }
}
