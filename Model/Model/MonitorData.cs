using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 监测数据
    /// </summary>
    [Serializable]
    public class MonitorData : DataModelBase, IMonitorData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        [Required]
        [Display(Name = "数据来源协议包ID")]
        public virtual Guid ProtocolDataId { get; set; }

        [Display(Name = "数据来源协议包")]
        [ForeignKey("ProtocolDataId")]
        public virtual ProtocolData ProtocolData { get; set; }

        [Required]
        [Display(Name = "数据类型ID")]
        public virtual Guid CommandDataId { get; set; }

        [Display(Name = "数据类型")]
        [ForeignKey("CommandDataId")]
        public virtual CommandData CommandData { get; set; }

        [Display(Name = "来源工地ID")]
        public Guid? ProjectId { get; set; }

        [Display(Name = "来源工地")]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Display(Name = "数据名称")]
        public virtual string DataName { get; set; }

        [Required]
        [Display(Name = "数据值")]
        public virtual double MonitorDataValue { get; set; }

        [Required]
        [Display(Name = "数据上传时间")]
        public virtual DateTime UpdateTime { get; set; }

        [Display(Name = "是否有效数据")]
        public virtual bool DataIsValid { get; set; }
    }
}