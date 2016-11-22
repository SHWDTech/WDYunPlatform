using System;
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
        /// 获取当前系统所有协议
        /// </summary>
        public static List<Protocol> AllProtocols => ProtocolsCache.Select(obj => obj.Value).ToList();

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
            foreach (var protocol in ProcessInvoke.Instance<ProtocolCodingProcess>().GetProtocolsFullLoaded()
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

            var protocol = ProcessInvoke.Instance<ProtocolCodingProcess>().GetProtocolFullLoadedByName(name);
            if (protocol == null) return null;

            ProtocolsCache.Add(protocol.ProtocolName, protocol);
            return ProtocolsCache[name];
        }

        /// <summary>
        /// 通过协议所属域获取协议集合
        /// </summary>
        /// <param name="fieldName">协议所属域名称</param>
        /// <returns></returns>
        public static List<Protocol> GerProtocolsByField(string fieldName)
        {

            var protocolList = ProtocolsCache.Where(protocol => protocol.Value.SubField.ItemValue == fieldName)
                .Select(obj => obj.Value)
                .ToList();
            if (protocolList.Count == 0)
            {
                protocolList = ProtocolsCache.Where(protocol => protocol.Value.Field.ItemValue == fieldName)
                    .Select(obj => obj.Value)
                    .ToList();
            }

            return protocolList;
        }

        /// <summary>
        /// 从协议缓存中获取指定ID的指令
        /// </summary>
        /// <param name="commandGuid"></param>
        /// <returns></returns>
        public static ProtocolCommand GetCommand(Guid commandGuid)
            => ProtocolsCache.Select(protocol => protocol.Value.ProtocolCommands.FirstOrDefault(cmd => cmd.Id == commandGuid))
            .FirstOrDefault(targetCommand => targetCommand != null);
    }
}
