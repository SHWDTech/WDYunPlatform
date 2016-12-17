using System;
using SHWDTech.Platform.ProtocolService.DataBase;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public abstract class ProtocolEncoder : IProtocolEncoder
    {
        public virtual IProtocolPackage Decode(byte[] bufferBytes)
        {
            throw new NotImplementedException();
        }

        public virtual IProtocolPackage Decode(byte[] bufferBytes, IProtocol protocol)
        {
            throw new NotImplementedException();
        }

        protected virtual void DecodeCommand(IProtocolPackage<string> package)
        {
            throw new NotImplementedException();
        }

        protected virtual void DetectCommand(IProtocolPackage<string> package, IProtocol matchedProtocol)
        {
            throw new NotImplementedException();
        }
    }
}
