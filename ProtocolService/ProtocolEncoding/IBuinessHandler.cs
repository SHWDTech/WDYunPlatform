namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    /// <summary>
    /// 业务处理程序
    /// </summary>
    public interface IBuinessHandler
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        string BusinessName { get; }

        /// <summary>
        /// 执行业务处理程序
        /// </summary>
        /// <param name="package"></param>
        void RunHandler(IProtocolPackage package);
    }
}
