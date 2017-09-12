using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    public class PlatformAccess : SysDomainModelBase, IPlatformAccess
    {
        [Index("Index_PlatformName", IsClustered = true)]
        [StringLength(256)]
        public string PlatformName { get; set; }

        public Guid TargetGuid { get; set; }

        public DateTime AccessTime { get; set; }
    }
}
