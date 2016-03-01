using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enum;

namespace SHWDTech.Platform.Model.ModelBase
{
    [Serializable]
    public class ModelBase : IModel.IModel
    {
        [Key]
        public virtual Guid Guid { get; set; }

        [NotMapped]
        public ModelState ModelState { get; set; } = ModelState.UnChanged;

        [NotMapped]
        public bool IsNew => ModelState == ModelState.Added;
    }
}
