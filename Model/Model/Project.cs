using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Project : SysModelBase, IProject
    {
        [Required]
        [Display(Name = "项目所属域")]
        public SysDomain ProjectDomain { get; set; }

        [Required]
        [Display(Name = "项目编码")]
        [MaxLength(25)]
        public string ProjectCode { get; set; }

        [Display(Name = "项目外部编码")]
        [MaxLength(25)]
        public string ProjectOutCode { get; set; }

        [Required]
        [Display(Name = "项目名称")]
        [MaxLength(25)]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "项目负责人")]
        [MaxLength(25)]
        public string ChargeMan { get; set; }

        [Display(Name = "负责人电话")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        public string Telephone { get; set; }

        [Display(Name = "项目坐标经度")]
        [Range(-180, 180)]
        public float Longitude { get; set; }

        [Display(Name = "项目坐标纬度")]
        [Range(-180, 180)]
        public float Latitude { get; set; }

        [Display(Name = "项目所属公司")]
        public string Compnany { get; set; }

        [Display(Name = "项目地址")]
        [MaxLength(50)]
        public string Address { get; set; }

        [Display(Name = "项目所在街道")]
        [MaxLength(200)]
        public string Street { get; set; }

        [Display(Name = "项目所属区域")]
        public District District { get; set; }

        [Display(Name = "项目面积")]
        public short Square { get; set; }

        [Display(Name = "项目开始时间")]
        public DateTime StartDate { get; set; }

        [Display(Name = "项目所属阶段")]
        public int Stage { get; set; }

        [Display(Name = "项目类型")]
        public int Type { get; set; }

        [Display(Name = "项目报警类型")]
        public int AlarmType { get; set; }

        [Display(Name = "项目是否启用")]
        public bool IsEnabled { get; set; }
    }
}
