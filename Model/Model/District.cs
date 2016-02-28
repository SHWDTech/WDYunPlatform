using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class District :SysModelBase, IDistrict
    {
        [Key]
        public int DistrictId { get; set; }

        public District ParentDistrict { get; set; }

        [Required]
        [MaxLength(50)]
        public string DistrictName { get; set; }

        [Required]
        [MaxLength(25)]
        public string DistrictType { get; set; }
    }
}
