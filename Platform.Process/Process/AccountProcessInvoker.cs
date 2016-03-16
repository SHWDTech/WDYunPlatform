using Platform.Process.IProcess;

namespace Platform.Process.Process
{
    public class AccountProcessInvoker
    {
        public IAccountProcess GetInstance() => new AccountProcess();
    }
}
