using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Constrains;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model
{
    [Serializable]
    public class GuidModel : IGuidModel
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual ModelState ModelState { get; set; }

        public virtual bool IsNew => ModelState == ModelState.Added;

    }
}
