namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议结果组件
    /// </summary>
    public class Component : IComponent
    {
        public string ComponentName { get; set; }

        public string DataType { get; set; }

        public byte[] ComponentData { get; set; }
    }
}