using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议数据处理接口
    /// </summary>
    public interface IProtocolDataProcess
    {
        /// <summary>
        /// 获取一个新的协议数据模型
        /// </summary>
        /// <returns></returns>
        ProtocolData GetNewProtocolData();
    }
}
