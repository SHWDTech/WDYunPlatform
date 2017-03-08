using Newtonsoft.Json;
using SHWDTech.Platform.Model.IModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 系统模型基类
    /// </summary>
    [Serializable]
    public class SysModelBase : ModelBase, ISysModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public virtual DateTime CreateDateTime { get; set; }

        [JsonIgnore]
        public virtual Guid CreateUserId { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public virtual DateTime? LastUpdateDateTime { get; set; }

        [JsonIgnore]
        public virtual Guid? LastUpdateUserId { get; set; }

        [JsonIgnore]
        public virtual bool IsDeleted { get; set; }

        [Display(Name = "是否启用")]
        public virtual bool IsEnabled { get; set; }
    }
}