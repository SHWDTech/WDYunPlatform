namespace SHWDTech.Platform.ChargingPileCommandCoder
{
    public class ControlCode
    {
        public ControlCode(ushort code)
        {
            NeedResponse = (code & (1 << 0xF)) == 1;
            ExceptionCode = (code & 0x7F00) >> 8;
            ResponsePorts = code & 0xFF;
        }

        public bool NeedResponse { get; }

        public int ExceptionCode { get; }

        public int ResponsePorts { get; }
    }
}
