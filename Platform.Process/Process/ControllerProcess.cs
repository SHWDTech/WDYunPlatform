using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using System.Web;

namespace Platform.Process.Process
{
    public class ControllerProcess : IControllerProcess
    {
        public WdUser GetCurrentUser(HttpContext context)
        {
            var repo = new UserRepository();

            return repo.GetUserByLoginName(context.User.Identity.Name);
        }
    }
}