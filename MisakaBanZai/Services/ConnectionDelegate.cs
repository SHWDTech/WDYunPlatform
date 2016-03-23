using System;
using MisakaBanZai.Models;

namespace MisakaBanZai.Services
{
    /// <summary>
    /// TCP客户端接收委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ClientAcceptEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 新连接添加委托
    /// </summary>
    public delegate void ConnectionAddEventHandler(object sender, MisakaConnectionEventArgs e);

    /// <summary>
    /// TCP客户端数据接收委托
    /// </summary>
    public delegate void ClientReceivedDataEventHandler();
}
