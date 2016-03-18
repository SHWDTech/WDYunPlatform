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
        private static Dictionary<Guid, IList<Protocol>> _deviceProtocolsCache; 
        #endregion

        /// <summary>
        /// 初始化协议信息
        /// </summary>
        public static void InitManager()
        {
            _deviceProtocolsCache = new Dictionary<Guid, IList<Protocol>>();
        }

        /// <summary>
        /// 获取设备对应的协议信息
        /// </summary>
        /// <param name="deviceGuid"></param>
        /// <returns></returns>
        public static IList<Protocol> GetDeviceProtocolsFullLoaded(Guid deviceGuid)
        {
            //先从缓存中读取协议信息，如果缓存中没有，再从数据库读取
            if (_deviceProtocolsCache.ContainsKey(deviceGuid)) return _deviceProtocolsCache[deviceGuid];

            var protocol = ProcessInvoke.GetInstance<ProtocolCodingProcess>().GetDeviceProtocolsFullLoaded(deviceGuid);

            foreach (var prot in protocol)
            {
                prot.ProtocolStructures = prot.ProtocolStructures.OrderBy(obj => obj.ComponentIndex).ToList();

                foreach (var command in prot.ProtocolCommands)
                {
                    command.CommandDatas = command.CommandDatas.OrderBy(obj => obj.DataIndex).ToList();
                }
            }

            _deviceProtocolsCache.Add(deviceGuid, protocol);

            return _deviceProtocolsCache[deviceGuid];
        }

        /// <summary>
        /// 协议帧头与字节流匹配
        /// </summary>
        /// <param name="protocolBytes">协议字节流</param>
        /// <param name="protocolHead">协议定义帧头</param>
        /// <returns>匹配返回TRUE，否则返回FALSE</returns>
        public static bool IsHeadMatched(IReadOnlyList<byte> protocolBytes, IReadOnlyList<byte> protocolHead)
        {
            var matched = true;

            for (var i = 0; i < protocolHead.Count; i++)
            {
                if (protocolBytes[i] != protocolHead[i]) matched = false;
            }

            return matched;
        }
    }
}
