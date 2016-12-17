namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    /// <summary>
    /// 客户端数据提供程序
    /// </summary>
    public interface IClientSourceProvider
    {
        /// <summary>
        /// 获取NODEID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        IClientSource GetClientSource(object nodeId);
    }
}
