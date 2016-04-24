namespace SHWDTech.Platform.Utility.Enum
{
    /// <summary>
    /// 报告消息枚举
    /// </summary>
    public static class ReportMessageEnum
    {
        /// <summary>
        /// 连接服务器失败
        /// </summary>
        public const string ConnectServerFailed = "连接服务器失败！";

        /// <summary>
        /// 发送数据失败
        /// </summary>
        public const string DataSendFailed = "发送数据失败!";

        /// <summary>
        /// 关闭套接字错误
        /// </summary>
        public const string SocketCloseException = "关闭套接字错误！";

        /// <summary>
        /// 接收客户端数据错误
        /// </summary>
        public const string ClientReceiveException = "接收客户端数据错误！";

        /// <summary>
        /// 收到空包
        /// </summary>
        public const string GetEmptyData = "收到空包！";

        /// <summary>
        /// 启动侦听失败
        /// </summary>
        public const string StartListenerFailed = "启动侦听失败！";

        /// <summary>
        /// 添加客户端成功
        /// </summary>
        public const string AcceptClientSuccess = "添加客户端成功！";

        /// <summary>
        /// 接收客户端请求失败
        /// </summary>
        public const string AcceptClientFailed = "接收客户端请求失败！";

        /// <summary>
        /// 侦听器已经关闭
        /// </summary>
        public const string ListenerClosed = "侦听器已经关闭！";

        /// <summary>
        /// 关闭套接字错误
        /// </summary>
        public const string CloseSocketException = "关闭套接字错误！";

        /// <summary>
        /// 与客户端的连接断开
        /// </summary>
        public const string ClientDisconnected = "与客户端的连接断开";
    }
}
