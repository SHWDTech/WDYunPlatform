using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process.Process
{
    public class DeviceModelProcess : ProcessBase, IDeviceModelProcess
    {
        public Dictionary<Guid, string> GetDeviceModelSelectList()
        {
            using (var repo = Repo<DeviceModelRepository>())
            {
                return repo.GetAllModels()
                    .ToDictionary(key => key.Id, value => value.Name);
            }
        }
    }
}
