using System;

namespace SHWDTech.Platform.PlatformServices
{
    /// <summary>
    /// 平台服务接口
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// 服务GUID
        /// </summary>
        Guid ServiceGuid { get; }

        /// <summary>
        /// 启动服务
        /// </summary>
        void Start(string path, Type[] formaterTypes);

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();

        /// <summary>
        /// 重启服务
        /// </summary>
        void ReStart();
    }
}
