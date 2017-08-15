using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ChargingPileCommandCoder
{
    public class ChargingPileProtocolPackage : BytesProtocolPackage
    {
        private OperateCode _operateCode;

        public OperateCode OperateCode
        {
            get
            {
                if (_operateCode != null) return _operateCode;
                if (!DataComponents.ContainsKey(nameof(OperateCode))) return null;
                _operateCode = new OperateCode(DataComponents[nameof(OperateCode)].ComponentContent[0]);
                return _operateCode;
            }
        }

        private ControlCode _controlCode;

        public ControlCode ControlCode
        {
            get
            {
                if (_controlCode != null) return _controlCode;
                if (!DataComponents.ContainsKey(nameof(ControlCode))) return null;
                var bytes = DataComponents[nameof(ControlCode)].ComponentContent;
                var controlValue = (ushort) ((bytes[1] << 8) + bytes[0]);
                _controlCode = new ControlCode(controlValue);
                return _controlCode;
            }
        }
    }
}
