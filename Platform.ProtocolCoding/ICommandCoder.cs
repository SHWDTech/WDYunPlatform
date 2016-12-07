using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding
{
    public interface ICommandCoder
    {
        /// <summary>
        /// 解析协议结构
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="matchedProtocol"></param>
        /// <returns></returns>
        IProtocolPackage DecodeProtocol(byte[] bufferBytes, Protocol matchedProtocol);

        /// <summary>
        /// 编码指定指令的协议
        /// </summary>
        /// <returns></returns>
        IProtocolPackage EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null);

        /// <summary>
        /// 执行协议包后续处理
        /// </summary>
        /// <param name="package"></param>
        /// <param name="source"></param>
        void DoDelive(IProtocolPackage package, IPackageSource source);
    }
}
