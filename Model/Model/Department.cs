using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 部门模型
    /// </summary>
    public class Department : SysDomainModelBase, IDepartment
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "部门名称")]
        public string Name { get; set; }

        [Display(Name = "备注")]
        public string Comment { get; set; }
    }
}
