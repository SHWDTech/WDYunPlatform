using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IFirmware
    {
        string FirmwareName { get; set; }

        ICollection<FirmwareSet> FirmwareSets { get; set; }

        ICollection<Protocol> Protocols { get; set; }
    }
}
