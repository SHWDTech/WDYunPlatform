using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SHWDTech.Platform.Model.IModel;

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
        public IUser CreateUser { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdateDateTime { get; set; }

        [JsonIgnore]
        public IUser LastUpdateUser { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        public bool IsEnabled { get; set; }
    }
}
