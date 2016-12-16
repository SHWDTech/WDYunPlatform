using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.StorageConstrains.Model.Enums;

namespace SHWDTech.Platform.StorageConstrains.Model
{
    [Serializable]
    public class GuidModel : IGuidModel
    {
        [Key]
        public virtual Guid Id { get; set; }

        [NotMapped]
        public virtual ModelState ModelState { get; set; }

        [NotMapped]
        public virtual bool IsNew => ModelState == ModelState.Added;

    }
}
