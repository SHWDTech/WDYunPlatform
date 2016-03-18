namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议组件对象
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// 组件名称
        /// </summary>
        string ComponentName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 组件数据
        /// </summary>
        byte[] ComponentData { get; set; }
    }
}
