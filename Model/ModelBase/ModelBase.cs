using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.ModelBase
{
    [Serializable]
    public class ModelBase : IModel.IModel
    {
        [Key]
        public virtual Guid Guid { get; set; }
    }
}
