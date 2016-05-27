using System.Collections.Generic;

namespace SHWDTech.Platform.ServiceHandler
{
    public interface IServiceHandler
    {
        void Execute(List<object> messageParams, string messageType);
    }
}
