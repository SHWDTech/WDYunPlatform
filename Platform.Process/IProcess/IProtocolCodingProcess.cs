using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议信息处理器接口
    /// </summary>
    public interface IProtocolCodingProcess
    {
        /// <summary>
        /// （同步）读取协议信息及协议结构
        /// </summary>
        /// <returns></returns>
        IList<Protocol> GetProtocolsFullLoaded();

        /// <summary>
        /// 通过协议名称获取协议
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Protocol GetProtocolByName(string name);

        /// <summary>
        /// （同步）通过协议名称获取协议所有信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Protocol GetProtocolFullLoadedByName(string name);
    }
}
