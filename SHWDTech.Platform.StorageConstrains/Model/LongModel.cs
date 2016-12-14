using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.StorageConstrains.Model.Enums;

namespace SHWDTech.Platform.StorageConstrains.Model
{
    [Serializable]
    public class LongModel : ILongModel
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual ModelState ModelState { get; set; }

        public virtual bool IsNew => ModelState == ModelState.Added;
    }
}
