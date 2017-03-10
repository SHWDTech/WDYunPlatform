using SHWDTech.Platform.Model.IModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 数据模型基类
    /// </summary>
    [Serializable]
    public class DataModelBase : ModelBase, IDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required]
        [Display(Name = "所属域ID")]
        public virtual Guid DomainId { get; set; }

    }
}