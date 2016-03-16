using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class ProtocolCodingProcess : IProtocolCodingProcess
    {
        public IList<Firmware> GetAllFirmwares()
        {
            var repo = new FirmwareRepository();

            return repo.GetAllModelList();
        }
    }
}
