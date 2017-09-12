using System.Linq;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    public interface IPlatformAccessProcess
    {
        IQueryable<PlatformAccess> GetPlatformAccessesByPlatformName(string platformName);
    }
}
