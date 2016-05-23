using System;
using Newtonsoft.Json;
using SHWDTech.Platform.PlatformServices;

namespace SHWDTech.Platform.AdminControlService
{
    /// <summary>
    /// 管理工具控制服务
    /// </summary>
    public class AdminControlService : PlatformService, IPlatformService
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        private static readonly Guid ServiceId;

        /// <summary>
        /// 创建新的管理工具控制服务
        /// </summary>
        static AdminControlService()
        {
            ServiceId = new Guid("A3524115-D485-47BF-983D-8746CAB1F9B4");
        }

        public Guid ServiceGuid => ServiceId;

        protected override void ProcessMessage(IServiceMessage message)
        {
            var messageContent = JsonConvert.DeserializeObject<AdminControlServiceMessage>(message.MessageObjectJson);


        }

        /// <summary>
        /// 注册新服务
        /// </summary>
        public static void RegisterService()
        {

        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        public static void UnRegistetService()
        {

        }
    }
}
