using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using System.Web;
using SHWD.Platform.Repository;

namespace Platform.Process.Process
{
    public class ControllerProcess : IControllerProcess
    {
        public WdUser GetCurrentUser(HttpContext context)
        {
            var repo = DbRepository.Repo<UserRepository>();

            return repo.GetUserByLoginName(context.User.Identity.Name);
        }
    }
}