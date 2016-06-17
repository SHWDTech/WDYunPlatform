using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 餐饮企业设备
    /// </summary>
    [Serializable]
    public class RestaurantDevice : Device, IRestaurantDevice
    {
        [Display(Name = "出厂日期")]
        [Required]
        public DateTime ProductionDateTime { get; set; }

        [Display(Name = "联系电话")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "采集频率")]
        [Required]
        public int CollectFrequency { get; set; }

        [Display(Name = "净化器名称")]
        [MaxLength(50)]
        public string CleanerName { get; set; }

        [Display(Name ="净化器类型")]
        [ForeignKey("CleanerTypeId")]
        public UserDictionary CleanerType { get; set; }

        [Display(Name = "净化器类型ID")]
        public Guid? CleanerTypeId { get; set; }

        [Display(Name = "净化器型号")]
        [MaxLength(200)]
        public string CleanerModel { get; set; }

        [Display(Name = "净化器厂家")]
        [MaxLength(200)]
        public string CleanerManufacturer { get; set; }

        [Display(Name = "净化器额定电压")]
        public double CleanerRatedVoltage { get; set; }

        [Display(Name = "净化器最大电流")]
        public double CleanerMaxCurrent { get; set; }

        [Display(Name = "净化器额定电流")]
        public double CleanerRatedCurrent { get; set; }

        [Display(Name = "净化器最小电流")]
        public double CleanerMinCurrent { get; set; }

        [Display(Name = "风机类型")]
        public string FanType { get; set; }

        [Display(Name = "风机厂家")]
        public string FanManufacturer { get; set; }

        [Display(Name = "风机额定电压")]
        public double FanRatedVoltage { get; set; }

        [Display(Name ="风机最大电流")]
        public double FanMaxCurrent { get; set; }

        [Display(Name = "风机额定电流")]
        public double FanRatedCurrent { get; set; }

        [Display(Name = "风机最小电流")]
        public double FanMinCurrent { get; set; }

        [Display(Name = "排风量")]
        public double FanDeliveryRate { get; set; }

        [Display(Name = "备注")]
        [MaxLength(2000)]
        public string Comment { get; set; }
    }
}
