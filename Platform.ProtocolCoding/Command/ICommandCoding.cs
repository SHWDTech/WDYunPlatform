using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Command
{
    public interface ICommandCoding
    {
        /// <summary>
        /// 解码协议指令相关信息
        /// </summary>
        /// <param name="package">协议包</param>
        /// <param name="matchedProtocol">对应的协议</param>
        /// <returns>协议解码完成后的长度</returns>
        void DecodeCommand(ProtocolPackage package, Protocol matchedProtocol);

        /// <summary>
        /// 编码指定指令的协议
        /// </summary>
        /// <returns></returns>
        ProtocolPackage EncodeCommand(ProtocolCommand command);
    }
}
