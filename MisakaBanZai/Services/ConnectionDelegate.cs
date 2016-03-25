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
    public delegate void ClientReceivedDataEventHandler(IMisakaConnection conn);

    /// <summary>
    /// TCP客户端连接断开委托
    /// </summary>
    /// <param name="conn"></param>
    public delegate void ClientDisconnectEventHandler(IMisakaConnection conn);

    /// <summary>
    /// 报告数据添加时间委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void ReportDataAddedEventHandler(EventArgs e);

    /// <summary>
    /// 连接信息更改事件
    /// </summary>
    /// <param name="conn"></param>
    public delegate void ConnectionModefiedEventHandler(IMisakaConnection conn);

    /// <summary>
    /// 数据发送事件
    /// </summary>
    /// <param name="count"></param>
    public delegate void DataSendEventHandler(int count);
}
