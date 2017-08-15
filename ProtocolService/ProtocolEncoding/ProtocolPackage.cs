using System;
using System.Collections.Generic;
using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public class ProtocolPackage : IProtocolPackage
    {
        public virtual bool Finalized { get; protected set; } = false;

        public virtual int PackageLenth { get; } = 0;

        public virtual IClientSource ClientSource { get; set; }

        public virtual DateTime ReceiveDateTime { get; set; }

        public virtual IProtocolData ProtocolData { get; set; }

        public IProtocol Protocol { get; set; }

        public IProtocolCommand Command { get; set; }

        public virtual int DataComponentCount { get; } = 0;

        public virtual string DeviceNodeId { get; set; } = string.Empty;

        public virtual List<string> DeliverParams => Command.CommandDeliverParams;

        public virtual PackageStatus Status { get; set; }

        public virtual void SetupProtocolData()
        {
            throw new NotImplementedException();
        }

        public virtual byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public virtual void Finalization()
        {
            throw new NotImplementedException();
        }
    }
}
