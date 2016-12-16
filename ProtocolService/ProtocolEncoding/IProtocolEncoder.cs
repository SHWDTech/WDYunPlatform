using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public interface IProtocolEncoder
    {
        IProtocolPackage Decode(byte[] bufferBytes, IProtocol protocol);

        void Delive(IProtocolPackage package, IActiveClient client);
    }
}
