using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 餐饮企业设备
    /// </summary>
    public interface IRestaurantDevice : IDevice
    {
        /// <summary>
        /// 出厂日期
        /// </summary>
        DateTime? ProductionDateTime { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        string Telephone { get; set; }

        /// <summary>
        /// 采集频率
        /// </summary>
        int CollectFrequency { get; set; }

        /// <summary>
        /// 净化器名称
        /// </summary>
        string CleanerName { get; set; }

        /// <summary>
        /// 净化器类型
        /// </summary>
        ClearnerType CleanerType { get; set; }

        /// <summary>
        /// 净化器型号
        /// </summary>
        string CleanerModel { get; set; }

        /// <summary>
        /// 净化器厂家
        /// </summary>
        string CleanerManufacturer { get; set; }

        /// <summary>
        /// 净化器额定电压
        /// </summary>
        double CleanerRatedVoltage { get; set; }

        /// <summary>
        /// 净化器最大电流
        /// </summary>
        double CleanerMaxCurrent { get; set; }

        /// <summary>
        /// 净化器额定电流
        /// </summary>
        double CleanerRatedCurrent { get; set; }

        /// <summary>
        /// 净化器最小电流
        /// </summary>
        double CleanerMinCurrent { get; set; }

        /// <summary>
        /// 风机型号
        /// </summary>
        string FanType { get; set; }

        /// <summary>
        /// 风机厂家
        /// </summary>
        string FanManufacturer { get; set; }

        /// <summary>
        /// 风机额定电压
        /// </summary>
        double FanRatedVoltage { get; set; }

        /// <summary>
        /// 风机最大电流
        /// </summary>
        double FanMaxCurrent { get; set; }

        /// <summary>
        /// 风机额定电流
        /// </summary>
        double FanRatedCurrent { get; set; }

        /// <summary>
        /// 风机最小电流
        /// </summary>
        double FanMinCurrent { get; set; }

        /// <summary>
        /// 排风量
        /// </summary>
        double FanDeliveryRate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        /// 设备终端号
        /// </summary>
        string DeviceTerminalCode { get; set; }

        /// <summary>
        /// 设备照片
        /// </summary>
        string Photo { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        LampblackDeviceModel LampblackDeviceModel { get; set; }

        /// <summary>
        /// 设备类型ID
        /// </summary>
        Guid? DeviceModelId { get; set; }

        /// <summary>
        /// 启用通道数
        /// </summary>
        int ChannelCount { get; }
    }
}
