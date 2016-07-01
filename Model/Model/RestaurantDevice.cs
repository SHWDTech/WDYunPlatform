using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 餐饮企业设备
    /// </summary>
    [Serializable]
    public class RestaurantDevice : Device, IRestaurantDevice
    {
        [Required]
        [Display(Name = "设备编号")]
        [MaxLength(25)]
        public override string DeviceCode { get; set; }

        [Display(Name = "出厂日期")]
        [Required]
        public virtual DateTime ProductionDateTime { get; set; }

        [Display(Name = "联系电话")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public virtual string Telephone { get; set; }

        [Display(Name = "采集频率")]
        [Required]
        public virtual int CollectFrequency { get; set; }

        [Display(Name = "净化器名称")]
        [MaxLength(50)]
        public virtual string CleanerName { get; set; }

        [Display(Name ="净化器类型")]
        public virtual ClearnerType CleanerType { get; set; }

        [Display(Name = "净化器型号")]
        [MaxLength(200)]
        public virtual string CleanerModel { get; set; }

        [Display(Name = "净化器厂家")]
        [MaxLength(200)]
        public virtual string CleanerManufacturer { get; set; }

        [Display(Name = "净化器额定电压")]
        public virtual double CleanerRatedVoltage { get; set; }

        [Display(Name = "净化器最大电流")]
        public virtual double CleanerMaxCurrent { get; set; }

        [Display(Name = "净化器额定电流")]
        public virtual double CleanerRatedCurrent { get; set; }

        [Display(Name = "净化器最小电流")]
        public virtual double CleanerMinCurrent { get; set; }

        [Display(Name = "风机类型")]
        public virtual string FanType { get; set; }

        [Display(Name = "风机厂家")]
        public virtual string FanManufacturer { get; set; }

        [Display(Name = "风机额定电压")]
        public virtual double FanRatedVoltage { get; set; }

        [Display(Name ="风机最大电流")]
        public virtual double FanMaxCurrent { get; set; }

        [Display(Name = "风机额定电流")]
        public virtual double FanRatedCurrent { get; set; }

        [Display(Name = "风机最小电流")]
        public virtual double FanMinCurrent { get; set; }

        [Display(Name = "排风量")]
        public virtual double FanDeliveryRate { get; set; }

        [Display(Name = "备注")]
        [MaxLength(2000)]
        public virtual string Comment { get; set; }

        [Required]
        [Display(Name = "设备名称")]
        [MaxLength(50)]
        public virtual string DeviceName { get; set; }

        [Required]
        [Display(Name = "设备终端号")]
        [MaxLength(50)]
        public virtual string DeviceTerminalCode { get; set; }

        [Display(Name = "设备照片")]
        public virtual string Photo { get; set; }

        [Display(Name = "IP地址")]
        public virtual string IpAddress { get; set; }
    }
}
