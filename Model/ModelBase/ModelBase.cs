using SHWDTech.Platform.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 模型类基类
    /// </summary>
    [Serializable]
    public class ModelBase : IModel.IModel
    {
        [Key]
        [Display(Name = "唯一标识符")]
        public virtual Guid Id { get; set; }

        [NotMapped]
        [Display(Name = "模型状态")]
        public ModelState ModelState { get; set; } = ModelState.UnChanged;

        [NotMapped]
        [Display(Name = "是否新创建对象")]
        public bool IsNew => ModelState == ModelState.Added;
    }
}