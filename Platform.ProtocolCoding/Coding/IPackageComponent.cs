using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 协议组件对象
    /// </summary>
    public interface IPackageComponent
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
        /// 数据索引
        /// </summary>
        int ComponentIndex { get; set; }

        /// <summary>
        /// 组件数据
        /// </summary>
        byte[] ComponentBytes { get; set; }

        /// <summary>
        /// 数据值类型
        /// </summary>
        DataValueType DataValueType { get; set; }

        /// <summary>
        /// 数据有效性验证位
        /// </summary>
        byte ValidFlag { get; set; }
    }
}
