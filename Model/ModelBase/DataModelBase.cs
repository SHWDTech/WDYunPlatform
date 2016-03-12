using SHWDTech.Platform.Model.IModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 数据模型基类
    /// </summary>
    [Serializable]
    public class DataModelBase : ModelBase, IDataModel
    {
        [Required]
        [Display(Name = "所属域ID")]
        public virtual Guid DomainId { get; set; }

        [Display(Name = "所属域")]
        [ForeignKey("DomainId")]
        public virtual Domain Domain { get; set; }
    }
}