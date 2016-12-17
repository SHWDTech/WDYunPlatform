using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public interface IProtocolEncoder
    {
        /// <summary>
        /// 解码协议数据
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <returns></returns>
        IProtocolPackage Decode(byte[] bufferBytes);

        /// <summary>
        /// 解码协议数据
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="protocol"></param>
        /// <returns></returns>
        IProtocolPackage Decode(byte[] bufferBytes, IProtocol protocol);
    }
}
