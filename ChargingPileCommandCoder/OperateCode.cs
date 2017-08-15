namespace SHWDTech.Platform.ChargingPileCommandCoder
{
    public class OperateCode
    {
        public OperateCode(byte code)
        {
            Action = (Action) (code & (1 << 7));
            Operate = (Operate) (code & 0x0F);
        }

        public Action Action { get; }

        public Operate Operate { get; }
    }

    public enum Action
    {
        Request = 0x00,

        Response = 0x01
    }

    public enum Operate
    {
        Read = 0x01,

        Write = 0x02,

        Control = 0x03,

        AutoUpload = 0x04
    }
}
