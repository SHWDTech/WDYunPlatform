using System;

namespace ProtocolServiceAbstraction
{
    public interface IProtocolService
    {
        void Send(byte[] data, Guid device);
    }
}
