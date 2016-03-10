using SHWDTech.Platform.Model.IModel;
using System.Web;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process.Process
{
    public class ControllerProcess
    {
        public IWdUser GetCurrentUser(HttpContext context)
        {
            var repo = new UserRepository();

            return repo.GetUserByName(context.User.Identity.Name);
        }
    }
}