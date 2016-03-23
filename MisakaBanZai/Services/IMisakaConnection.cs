using System.Collections.Generic;
using MisakaBanZai.Models;

namespace MisakaBanZai.Services
{
    public interface IMisakaConnection
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        string ConnectionName { get; set; }

        /// <summary>
        /// 连接类型
        /// </summary>
        string ConnectionType { get; set; }

        /// <summary>
        /// 连接对象
        /// </summary>
        object ConnObject { get; }

        /// <summary>
        /// 所属窗体
        /// </summary>
        IMisakaConnectionManagerWindow ParentWindow { get; set; }

        /// <summary>
        /// 客户端接收数据事件
        /// </summary>
        event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        /// <summary>
        /// 输出套接字字节流
        /// </summary>
        /// <returns></returns>
        byte[] OutPutSocketBytes();

        /// <summary>
        /// 数据处理缓存
        /// </summary>
        IList<byte> ProcessBuffer { get; }

        /// <summary>
        /// 发送字节流
        /// </summary>
        /// <param name="bytes"></param>
        void Send(byte[] bytes);
    }
}
