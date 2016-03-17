using System;
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

            _deviceProtocolsCache.Add(deviceGuid, ProcessInvoke.GetInstance<ProtocolCodingProcess>().GetDeviceProtocolsFullLoaded(deviceGuid));

            return _deviceProtocolsCache[deviceGuid];
        }
    }
}
