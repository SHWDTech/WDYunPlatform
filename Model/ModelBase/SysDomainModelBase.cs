using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.ModelBase
{
    public class SysDomainModelBase : SysModelBase, IDomainModel
    {
        [Required]
        [Display(Name = "所属域")]
        public ISysDomain Domain { get; set; }
    }
}
