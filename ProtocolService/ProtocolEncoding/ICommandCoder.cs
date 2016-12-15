namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public interface ICommandCoder
    {
        void DecodeCommand(IProtocolPackage package);
    }
}
