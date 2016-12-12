namespace SHWDTech.Platform.ProtocolService
{
    /// <summary>
    /// 服务宿主接口
    /// </summary>
    public interface IServiceHost
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();

        /// <summary>
        /// 重新启动服务
        /// </summary>
        void Restart();

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();

        /// <summary>
        /// 删除客户端对象
        /// </summary>
        /// <param name="client"></param>
        void RemoveClient(IActiveClient client);
    }
}
