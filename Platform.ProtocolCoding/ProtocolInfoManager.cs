using System.Collections.Generic;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议信息管理工具
    /// </summary>
    public static class ProtocolInfoManager
    {
        #region Fields
        /// <summary>
        /// 设备对应协议信息缓存
        /// </summary>
        public static List<Protocol> ProtocolsCache { get; } = new List<Protocol>(); 
        #endregion

        /// <summary>
        /// 初始化协议信息
        /// </summary>
        public static void InitManager()
        {
            GetProtocolsFullLoaded();
        }

        /// <summary>
        /// 获取所有协议信息
        /// </summary>
        /// <returns></returns>
        private static void GetProtocolsFullLoaded()
        {
            foreach (var protocol in ProcessInvoke.GetInstance<ProtocolCodingProcess>().GetProtocolsFullLoaded())
            {
                if (!ProtocolsCache.Contains(protocol))
                {
                    ProtocolsCache.Add(protocol);
                }
            }
        }
    }
}
