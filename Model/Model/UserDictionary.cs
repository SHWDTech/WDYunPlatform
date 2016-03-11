using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 用户自定义字典
    /// </summary>
    [Serializable]
    public class UserDictionary : SysDomainModelBase, IUserDictionary
    {
        [Required]
        [Display(Name = "字典名称")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "字典项关键字")]
        public string ItemKey { get; set; }

        [Display(Name = "字典项值")]
        public string ItemValue { get; set; }

        [Display(Name = "字典项层级")]
        public int ItemLevel { get; set; }

        [Display(Name = "父级字典项")]
        public virtual SysDictionary ParentDictionary { get; set; }
    }
}