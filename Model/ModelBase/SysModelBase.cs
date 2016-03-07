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
        public DateTime CreateDateTime { get; set; }

        [JsonIgnore]
        public IWdUser CreateUser { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdateDateTime { get; set; }

        [JsonIgnore]
        public IWdUser LastUpdateUser { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        public bool IsEnabled { get; set; }
    }
}