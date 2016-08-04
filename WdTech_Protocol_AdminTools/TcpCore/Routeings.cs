using System;
using System.Messaging;
using System.Timers;
using Platform.MessageQueue.Models;
using SHWDTech.Platform.Utility;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    public class Routeings
    {
        private readonly MessageQueue _routeingMessageQueue;

        private readonly MessageQueue _commandDispatchQueue;

        private readonly Timer _routeingTimer;

        public Routeings()
        {
            _routeingMessageQueue = new MessageQueue(@"FormatName:Direct=TCP:114.55.175.99\private$\deviceconnectStatus");
            _commandDispatchQueue = new MessageQueue(@"FormatName:Direct=TCP:114.55.175.99\private$\commandDispatch");

            _routeingTimer = new Timer(10000)
            {
                Enabled = true
            };
            _routeingTimer.Elapsed += ExecuteRouteings;
        }

        public void StartRouteings()
        {
            _routeingTimer.Start();
            try
            {
                _commandDispatchQueue.BeginReceive(MessageQueue.InfiniteTimeout, _commandDispatchQueue,
                    CommandDispatcher);

            }
            catch (Exception ex)
            {
                LogService.Instance.Error("123", ex);
            }
        }

        public void StopRouteings()
        {
            _routeingTimer.Stop();
        }

        private void ExecuteRouteings(object sender, ElapsedEventArgs e)
        {
            try
            {
                var existMessages = _routeingMessageQueue.GetAllMessages();
                foreach (var existMessage in existMessages)
                {
                    _routeingMessageQueue.ReceiveById(existMessage.Id);
                }
                var message = new Message()
                {
                    Formatter = new XmlMessageFormatter(new[] { typeof(DeviceConnectingStatus) }),
                    Body = new DeviceConnectingStatus(CommunicationServices.GetManagerDevices())
                };

                _routeingMessageQueue.Send(message);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("访问消息队列失败。", ex);
            }
        }

        private void CommandDispatcher(IAsyncResult asyncResult)
        {
            var queue = (MessageQueue) asyncResult.AsyncState;

            var message = queue.EndReceive(asyncResult);

            if (!(message.Body is CommandDisptcherModel)) return;

            var mission = (CommandDisptcherModel) message.Body;

            CommunicationServices.SendCommand(mission.DeviceGuid, mission.CommandGuid);
        }
    }
}
