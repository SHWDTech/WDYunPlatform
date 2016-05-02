using System;
using System.Messaging;
using SHWDTech.Platform.ProtocolCoding.MessageQueueModel;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new MessageQueue(@"FormatName:Direct=TCP:121.40.49.97\private$\protocolcommand")
            {
                Formatter = new XmlMessageFormatter(new[] {typeof(CommandMessage)})
            };

            var msg = new CommandMessage()
            {
                DeviceGuid = Guid.Parse("ba0ca1dc-0331-4d5a-96e5-49ac20665a13"),
                CommandGuid = Guid.Parse("52FD2857-1607-4AD6-86C9-AC6B2B75BBB6"),
                Params = null
            };

            queue.Send(msg);

            Console.WriteLine(@"Send Complete");

            Console.ReadKey();
        }
    }
}
