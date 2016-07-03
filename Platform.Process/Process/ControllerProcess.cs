using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using System.Web;
using Platform.Cache;
using Platform.Process.Enums;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.Process
{
    public class ControllerProcess : ProcessBase, IControllerProcess
    {
        public WdUser GetCurrentUser(HttpContext context) 
            => GeneralProcess.GetUserByLoginName(context.User.Identity.Name);

        public Dictionary<string, string> GetBasePageInformation(IWdUser user)
        {
            var cache = PlatformCaches.GetCache($"{user.DomainId}-{SystemCacheNames.DomainCompany}");

            if (cache != null)
            {
                return (Dictionary<string, string>) cache.CacheItem;
            }

            var repo = Repo<SysConfigRepository>();

            var information = repo.GetSysConfigDictionary(config => config.SysConfigType == SystemConfigType.DomainCompanyConfig);

            PlatformCaches.Add($"{user.DomainId}-{SystemCacheNames.DomainCompany}", information);

            return information;
        }
    }
}