using System;
using WdTech_Protocol_AdminTools.TcpCore;

namespace WdTech_Protocol_AdminTools.Services
{
    /// <summary>
    /// 报告数据添加时间委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void ReportDataAddedEventHandler(EventArgs e);

    /// <summary>
    /// TCP客户端数据接收委托
    /// </summary>
    public delegate void ClientReceivedDataEventHandler(TcpClientManager client);

    /// <summary>
    /// TCP客户端连接断开委托
    /// </summary>
    /// <param name="conn"></param>
    public delegate void ClientDisconnectEventHandler(TcpClientManager client);
}
