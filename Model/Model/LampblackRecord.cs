using System;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    public class LampblackRecord : DataModelBase
    {
        [Index("Ix_Project_Device_RecordDateTime", IsClustered = true, Order = 0)]
        public long ProjectIdentity { get; set; }

        [Index("Ix_Project_Device_RecordDateTime", IsClustered = true, Order = 1)]
        public long DeviceIdentity { get; set; }

        public long ProtocolId { get; set; }

        public bool CleanerSwitch { get; set; } = false;

        public int CleanerCurrent { get; set; } = 0;

        public bool FanSwitch { get; set; } = false;

        public int FanCurrent { get; set; } = 0;

        public int LampblackIn { get; set; } = 0;

        public int LampblackOut { get; set; } = 0;

        [Index("Ix_Project_Device_RecordDateTime", IsClustered = true, Order = 2)]
        public DateTime RecordDateTime { get; set; }
    }
}
