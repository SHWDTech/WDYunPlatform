using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SHWDTech.Platform.ProtocolService
{
    /// <summary>
    /// 服务宿主基类
    /// </summary>
    public class ServiceHost : IServiceHost
    {
        /// <summary>
        /// 服务宿主SOCKET
        /// </summary>
        protected Socket HostSocket;

        /// <summary>
        /// 服务是否在运行中
        /// </summary>
        protected bool IsRunning;

        /// <summary>
        /// 服务启动时间
        /// </summary>
        public DateTime StartDateTime { get; protected set; }

        /// <summary>
        /// 已连接客户端套接字
        /// </summary>
        protected List<IActiveClient> ActiveClients { get; set; }

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveClient(IActiveClient client)
        {
            throw new NotImplementedException();
        }
    }
}
