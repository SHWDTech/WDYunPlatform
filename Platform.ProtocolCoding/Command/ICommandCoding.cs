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
        /// <returns>解码完成后的协议包</returns>
        ProtocolPackage DecodeCommand(out ProtocolPackage package);

        /// <summary>
        /// 编码指定指令的协议
        /// </summary>
        /// <returns></returns>
        ProtocolPackage EncodeCommand(ProtocolCommand command);
    }
}
