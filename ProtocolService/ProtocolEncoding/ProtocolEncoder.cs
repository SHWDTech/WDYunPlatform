using System;
using SHWDTech.Platform.ProtocolService.DataBase;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public abstract class ProtocolEncoder : IProtocolEncoder
    {
        public IProtocol Protocol { get; set; }

        public virtual IProtocolPackage Decode(byte[] bufferBytes)
        {
            throw new NotImplementedException();
        }

        public virtual IProtocolPackage Decode(byte[] bufferBytes, IProtocol protocol)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将协议按照检测到的指令进行解码
        /// </summary>
        /// <param name="package"></param>
        protected virtual void DecodeCommand(IProtocolPackage<string> package)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 检测协议对应指令
        /// </summary>
        /// <param name="package"></param>
        /// <param name="matchedProtocol"></param>
        protected virtual void DetectCommand(IProtocolPackage<string> package, IProtocol matchedProtocol)
        {
            throw new NotImplementedException();
        }
    }
}
