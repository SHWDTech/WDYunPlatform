using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics
{
    public class PackageComponent<T> : IPackageComponent<T>
    {
        public string ComponentName { get; set; }

        public string DataType { get; set; }

        public int ComponentIndex { get; set; }

        public byte[] ComponentBytes { get; set; }

        public DataValueType DataValueType { get; set; }

        public byte ValidFlag { get; set; }

        public T ComponentContent { get; set; }

        public T ComponentValue { get; set; }
    }
}
