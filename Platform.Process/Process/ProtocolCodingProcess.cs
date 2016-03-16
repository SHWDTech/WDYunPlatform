using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class ProtocolCodingProcess : IProtocolCodingProcess
    {
        /// <summary>
        /// 读取所有固件信息以及关联的固件集和协议
        /// </summary>
        /// <returns>所有固件信息，以及关联的固件集和协议</returns>
        public IList<Firmware> GetAllFirmwares()
        {
            var repo = DbRepository.Repo<FirmwareRepository>();

            return repo.GetFirmwaresFullLoaded();
        }
    }
}
