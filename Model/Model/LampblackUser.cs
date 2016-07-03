using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 油烟用户模型
    /// </summary>
    public class LampblackUser : WdUser, ILampblackUser
    {
        [Display(Name = "用户部门")]
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [Display(Name = "用户部门ID")]
        public Guid DepartmentId { get; set; }

        [Display(Name = "用户机构")]
        [ForeignKey("CateringCompanyId")]
        public CateringCompany CateringCompany { get; set; }

        [Display(Name = "用户机构ID")]
        public Guid CateringCompanyId { get; set; }
    }
}
