using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Command
{
    public interface ICommandCoding
    {
        /// <summary>
        /// 解码协议指令相关信息
        /// </summary>
        /// <param name="package">协议包</param>
        /// <returns>协议解码完成后的长度</returns>
        void DecodeCommand(IProtocolPackage package);

        /// <summary>
        /// 编码指定指令的协议
        /// </summary>
        /// <returns></returns>
        IProtocolPackage EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null);

        /// <summary>
        /// 检测协议包所属指令
        /// </summary>
        /// <param name="package">协议包</param>
        /// <param name="matchedProtocol">对应的协议</param>
        void DetectCommand(IProtocolPackage package, IProtocol matchedProtocol);
    }
}
