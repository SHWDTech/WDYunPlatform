using System;
using SHWDTech.Platform.PlatformServices;

namespace SHWDTech.Platform.AdminControlService
{
    /// <summary>
    /// 管理工具控制服务
    /// </summary>
    public class AdminControlService : PlatformService
    {
        public static Guid ServiceGuid { get; }

        /// <summary>
        /// 创建新的管理工具控制服务
        /// </summary>
        static AdminControlService()
        {
            ServiceGuid = new Guid("A3524115-D485-47BF-983D-8746CAB1F9B4");
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public override void Start(string path, Type[] formaterTypes)
        {

        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public override void Stop()
        {

        }

        /// <summary>
        /// 重启服务
        /// </summary>
        public override void ReStart()
        {

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
