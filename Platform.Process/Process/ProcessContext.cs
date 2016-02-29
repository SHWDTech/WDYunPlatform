using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.Process
{
    public class ProcessContext : IProcessContext
    {
        public IUser CurrentUser { get; set; }

        public ISysDomain CurrentDomain { get; set; }
    }
}
