using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SHWDTech.Platform.Model.Interface;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.ModelBase
{
    [Serializable]
    public class SysModelBase : ModelBase, ISysModel
    {

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime CreateDateTime { get; set; }

        [JsonIgnore]
        public User CreateUser { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdateDateTime { get; set; }

        [JsonIgnore]
        public User LastUpdateUser { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
