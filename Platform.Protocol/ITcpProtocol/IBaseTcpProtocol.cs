using System;

namespace Platform.Protocol.ITcpProtocol
{
    public interface IBaseTcpProtocol
    {
        byte[] ProtocolContent { get; set; }

        int TaskId { get; set; }

        int ProtocolLength { get; set; }

        Guid DeviceId { get; set; }

        Guid ProtocolId { get; set; }

        Guid DomainId { get; set; }

        DateTime ReceiveDateTime { get; set; }

        void DecodeFrame();

        void EncodeFrame(byte[] protocolContent);
    }
}