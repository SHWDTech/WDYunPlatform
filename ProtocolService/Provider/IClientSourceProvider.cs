namespace SHWDTech.Platform.ProtocolService.Provider
{
    /// <summary>
    /// 客户端信息提供器接口
    /// </summary>
    public interface IClientSourceProvider
    {
        /// <summary>
        /// 获取NODEID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        IClientSource GetClientSource<T>(T nodeId);
    }
}
