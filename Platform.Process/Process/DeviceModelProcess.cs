using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process.Process
{
    public class DeviceModelProcess : ProcessBase, IDeviceModelProcess
    {
        public Dictionary<string, string> GetDeviceModelSelectList()
        {
            using (var repo = Repo<DeviceModelRepository>())
            {
                return repo.GetAllModels()
                    .ToDictionary(key => key.Name, value => value.Id.ToString());
            }
        }
    }
}
