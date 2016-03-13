using Newtonsoft.Json;
using SHWDTech.Platform.Model.IModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 系统模型基类
    /// </summary>
    [Serializable]
    public class SysModelBase : ModelBase, ISysModel
    {
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

        public virtual bool IsEnabled { get; set; }
    }
}