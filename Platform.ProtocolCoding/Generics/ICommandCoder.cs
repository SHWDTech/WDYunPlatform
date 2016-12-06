using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    public interface ICommandCoder<T>
    {
        IDataConverter<T> DataConverter { get; set; }

            /// <summary>
        /// 解析协议结构
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="matchedProtocol"></param>
        /// <returns></returns>
        IProtocolPackage DecodeProtocol(byte[] bufferBytes, Protocol matchedProtocol);

        /// <summary>
        /// 解码协议指令相关信息
        /// </summary>
        /// <param name="package">协议包</param>
        /// <returns>协议解码完成后的长度</returns>
        void DecodeCommand(IProtocolPackage<T> package);

        /// <summary>
        /// 编码指定指令的协议
        /// </summary>
        /// <returns></returns>
        IProtocolPackage<T> EncodeCommand(IProtocolCommand command, Dictionary<string, byte[]> paramBytes = null);

        /// <summary>
        /// 检测协议包所属指令
        /// </summary>
        /// <param name="package">协议包</param>
        /// <param name="matchedProtocol">对应的协议</param>
        void DetectCommand(IProtocolPackage<T> package, IProtocol matchedProtocol);

        /// <summary>
        /// 执行协议包后续处理
        /// </summary>
        /// <param name="package"></param>
        /// <param name="source"></param>
        void DoDelive(IProtocolPackage package, IPackageSource source);
    }
}
