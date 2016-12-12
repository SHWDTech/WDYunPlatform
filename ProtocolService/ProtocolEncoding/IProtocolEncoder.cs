namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public interface IProtocolEncoder
    {
        IProtocolPackage Decode(byte[] bufferBytes);

        void Delive(IProtocolPackage package, IActiveClient client);
    }
}
