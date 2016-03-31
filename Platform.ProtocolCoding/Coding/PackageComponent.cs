namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议结果组件
    /// </summary>
    public class PackageComponent : IPackageComponent
    {
        public string ComponentName { get; set; }

        public string DataType { get; set; }

        public int ComponentIndex { get; set; }

        public byte[] ComponentData { get; set; }
    }
}