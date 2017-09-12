using System;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class PlatformAccessProcess : ProcessBase, IPlatformAccessProcess
    {
        public IQueryable<PlatformAccess> GetPlatformAccessesByPlatformName(string platformName)
        {
            return Repo<PlatformRepository>().GetModels(p => p.PlatformName == platformName);
        }

        public void AddNoewPlatformAccessRegister(string platformName, Guid targetGuid)
        {
            var access = Repo<PlatformRepository>().CreateDefaultModel();
            access.AccessTime = DateTime.Now;
            access.TargetGuid = targetGuid;
            access.PlatformName = platformName;

            Repo<PlatformRepository>().AddOrUpdateDoCommit(access);
        }
    }
}
