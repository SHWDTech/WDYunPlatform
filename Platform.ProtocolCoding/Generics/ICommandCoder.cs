using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    public interface ICommandCoder<T> : ICommandCoder
    {
        IDataConverter<T> DataConverter { get; set; }

        /// <summary>
        /// 解码协议指令相关信息
        /// </summary>
        /// <param name="package">协议包</param>
        /// <returns>协议解码完成后的长度</returns>
        void DecodeCommand(IProtocolPackage<T> package);

        /// <summary>
        /// 检测协议包所属指令
        /// </summary>
        /// <param name="package">协议包</param>
        /// <param name="matchedProtocol">对应的协议</param>
        void DetectCommand(IProtocolPackage<T> package, IProtocol matchedProtocol);
    }
}
