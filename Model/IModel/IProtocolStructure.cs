using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议结构接口
    /// </summary>
    public interface IProtocolStructure : ISysModel
    {
        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Procotol { get; set; }

        /// <summary>
        /// 所属协议父结构
        /// </summary>
        ProtocolStructure Parenttructure { get; set; }

        /// <summary>
        /// 协议段名称
        /// </summary>
        string ComponentName { get; set; }

        /// <summary>
        /// 协议段索引值
        /// </summary>
        int ComponentIndex { get; set; }

        /// <summary>
        /// 协议段长度
        /// </summary>
        int ComponentLength { get; set; }

        /// <summary>
        /// 协议段数据长度
        /// </summary>
        int ComponentDataLength { get; set; }
    }
}