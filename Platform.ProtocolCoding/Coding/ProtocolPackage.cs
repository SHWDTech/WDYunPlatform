using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    public class ProtocolPackage : IProtocolPackage
    {
        public bool Finalized { get; } = false;

        public int PackageLenth { get; } = 0;

        public Device Device { get; set; }

        public DateTime ReceiveDateTime { get; set; }

        public ProtocolData ProtocolData { get; set; }

        public Protocol Protocol { get; set; }

        public IProtocolCommand Command { get; set; }

        public int DataComponentCount { get; } = 0;

        public string DeviceNodeId { get; set; } = string.Empty;

        public List<string> DeliverParams { get; } = null;

        public PackageStatus Status { get; set; }

        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }
    }
}
