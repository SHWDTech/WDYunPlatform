using System.Collections.Generic;
using System.Linq;
using System.Net;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using SHWDTech.Platform.Utility;

namespace MisakaBanZai.Services
{
    /// <summary>
    /// 数据连接管理器
    /// </summary>
    public static class ConnectionManager
    {
        /// <summary>
        /// 连接列表
        /// </summary>
        private static readonly IList<IMisakaConnection> MisakaConnections = new List<IMisakaConnection>();
        
        /// <summary>
        /// 新连接添加事件
        /// </summary>
        public static event ConnectionAddEventHandler ConnectionAddEvent;

        /// <summary>
        /// 添加链接
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static bool AddConnection(IMisakaConnection connection)
        {
            if (MisakaConnections.Contains(connection))
            {
                return false;
            }

            MisakaConnections.Add(connection);
            ConnectionAdded(null, new MisakaConnectionEventArgs() { Connection = connection});
            return true;
        }

        /// <summary>
        /// 添加了一个新连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnectionAdded(object sender, MisakaConnectionEventArgs e)
        {
            ConnectionAddEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// 移除连接
        /// </summary>
        /// <param name="connName"></param>
        public static void ConnectionRemove(string connName)
        {
            var conn = MisakaConnections.FirstOrDefault(obj => obj.ConnectionName == connName);
            if (conn != null)
            MisakaConnections.Remove(conn);
        }

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IMisakaConnection NewMisakaConnection(string type, IPAddress ipAddress, int port)
        {
            switch (type)
            {
                case ConnectionItemType.TcpServer:
                    return new MisakaTcpServer(ipAddress, port) {ConnectionType = type};
                case ConnectionItemType.TcpClient:
                    return new MisakaTcpClient(Globals.GetLocalIpAddress(), Globals.RandomPort()) {ConnectionType = type};
                default:
                    return null;
            }
        }
    }
}
