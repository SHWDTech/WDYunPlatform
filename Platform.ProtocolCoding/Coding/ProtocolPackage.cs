using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    public class ProtocolPackage : IProtocolPackage
    {
        public virtual bool Finalized { get; protected set; } = false;

        public virtual int PackageLenth { get; } = 0;

        public virtual Device Device { get; set; }

        public virtual DateTime ReceiveDateTime { get; set; }

        public virtual ProtocolData ProtocolData { get; set; }

        public virtual Protocol Protocol { get; set; }

        public virtual IProtocolCommand Command { get; set; }

        public virtual int DataComponentCount { get; } = 0;

        public virtual string DeviceNodeId { get; set; } = string.Empty;

        public virtual List<string> DeliverParams => Command.CommandDeliverParams;

        public virtual PackageStatus Status { get; set; }

        public virtual bool NeedReply => Command.CommandCategory == CommandCategory.Authentication ||
                                 Command.CommandCategory == CommandCategory.HeartBeat;

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
