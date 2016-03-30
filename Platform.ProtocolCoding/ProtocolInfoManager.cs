using System.Collections.Generic;
using System.Linq;
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
        private static Dictionary<string, Protocol> ProtocolsCache { get; } = new Dictionary<string, Protocol>();
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
            foreach (var protocol in ProcessInvoke.GetInstance<ProtocolCodingProcess>().GetProtocolsFullLoaded()
                .Where(protocol => !ProtocolsCache.ContainsValue(protocol)))
            {
                ProtocolsCache.Add(protocol.ProtocolName, protocol);
            }
        }

        /// <summary>
        /// 通过协议名称获取协议
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Protocol GetProtocolByName(string name)
        {
            if (ProtocolsCache.ContainsKey(name))
            {
                return ProtocolsCache[name];
            }

            var protocol = ProcessInvoke.GetInstance<ProtocolCodingProcess>().GetProtocolByName(name);
            if (protocol == null) return null;

            ProtocolsCache.Add(protocol.ProtocolName, protocol);
            return ProtocolsCache[name];
        }
    }
}
