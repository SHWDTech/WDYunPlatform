using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议结果组件
    /// </summary>
    public class PackageComponent : IPackageComponent
    {
        public string ComponentName { get; set; }

        public short ComponentChannel { get; set; }

        public CommandData CommandData { get; set; }

        public string DataType { get; set; }

        public int ComponentIndex { get; set; }

        public byte[] ComponentBytes { get; set; }

        public DataValueType DataValueType { get; set; }

        public byte ValidFlag { get; set; }
    }
}