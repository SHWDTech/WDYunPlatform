namespace SHWDTech.Platform.ProtocolService
{
    /// <summary>
    /// 服务宿主接口
    /// </summary>
    public interface IServiceHost
    {
        /// <summary>
        /// 服务宿主是否在运行中
        /// </summary>
        ServiceHostStatus Status { get; }

        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();

        /// <summary>
        /// 重新启动服务
        /// </summary>
        void Restart();

        /// <summary>
        /// 关闭服务
        /// </summary>
        void Close();

        /// <summary>
        /// 删除客户端对象
        /// </summary>
        /// <param name="client"></param>
        void RemoveClient(IActiveClient client);
    }
}
