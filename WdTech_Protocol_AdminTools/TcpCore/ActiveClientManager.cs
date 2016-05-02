using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net.Sockets;
using System.Transactions;
using Platform.Process;
using Platform.Process.Process;
using Platform.WdQueue;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.ProtocolCoding.MessageQueueModel;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Services;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 活动客户端管理器
    /// </summary>
    public class ActiveClientManager
    {
        /// <summary>
        /// 未认证的客户端连接
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly List<TcpClientManager> _clientSockets
            = new List<TcpClientManager>();

        /// <summary>
        /// 等待处理的任务
        /// </summary>
        private readonly List<Task> _responingTasks = new List<Task>();

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.RemoteEndPoint}" };
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            lock (_clientSockets)
            {
                _clientSockets.Add(tcpClientManager);
            }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpClient">客户端连接</param>
        private static void ClientDisconnected(TcpClientManager tcpClient)
        {
            AdminReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpClient.ReceiverName}");
        }

        /// <summary>
        /// 发送相关指令
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asyncResult"></param>
        public void MessagePeekCompleted(object source, PeekCompletedEventArgs asyncResult)
        {
            var messageQueue = (WdTechTask) source;

            var message = messageQueue.EndPeek(asyncResult.AsyncResult);

            var commandMessage = (CommandMessage) message.Body;
            if (commandMessage == null) return;

            using (var scope = new TransactionScope())
            {
                try
                {
                    var task = ProcessInvoke.GetInstance<TaskProcess>().GetTaskByGuid(commandMessage.TaskGuid);

                    SendCommand(commandMessage);

                    if (task != null)
                    {
                        task.ExecuteStatus = TaskExceteStatus.Sended;
                        _responingTasks.Add(task);
                    }

                    messageQueue.ReceiveById(message.Id);
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("发送指令错误！", ex);
                    return;
                }

                scope.Complete();
            }
        }

        private void CommandResponsed()
        {
            var task = _responingTasks.FirstOrDefault();
            task.ExecuteStatus = TaskExceteStatus.Responsed;
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="message">指令消息</param>
        public void SendCommand(CommandMessage message)
        {
            TcpClientManager device;
            lock (_clientSockets)
            {
                device = _clientSockets.FirstOrDefault(client => client.DeviceGuid == message.DeviceGuid);
            }

            if (device == null) return;

            var command = ProtocolInfoManager.GetCommand(message.CommandGuid);

            if (command == null) return;

            device.Send(command, message.Params);
        }
    }
}
