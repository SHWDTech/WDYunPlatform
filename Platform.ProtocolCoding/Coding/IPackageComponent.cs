using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

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
        /// 组件通道号
        /// </summary>
        short ComponentChannel { get; set; }

        /// <summary>
        /// 组件相关指令数据类型
        /// </summary>
        CommandData CommandData { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 数据索引
        /// </summary>
        int ComponentIndex { get; set; }

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
