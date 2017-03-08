using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 模型类基类
    /// </summary>
    [Serializable]
    public class ModelBase : IModel.IModel
    {
        [NotMapped]
        [Display(Name = "模型状态")]
        public virtual ModelState ModelState { get; set; } = ModelState.UnChanged;

        [NotMapped]
        [Display(Name = "是否新创建对象")]
        public virtual bool IsNew => ModelState == ModelState.Added;
    }
}