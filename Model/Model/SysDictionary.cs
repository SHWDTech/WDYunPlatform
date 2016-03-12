using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 系统自定义字典
    /// </summary>
    [Serializable]
    public class SysDictionary : SysModelBase, ISysDictionary
    {
        [Required]
        [Display(Name = "字典名称")]
        public virtual string ItemName { get; set; }

        [Required]
        [Display(Name = "字典项关键字")]
        public virtual string ItemKey { get; set; }

        [Display(Name = "字典项值")]
        public virtual string ItemValue { get; set; }

        [Display(Name = "字典项层级")]
        public virtual int ItemLevel { get; set; }

        [Display(Name = "父级字典项")]
        public virtual SysDictionary ParentDictionary { get; set; }
    }
}