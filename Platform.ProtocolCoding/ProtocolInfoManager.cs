using System.Collections.Generic;
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

        public static IList<Firmware> Firmwares { get; private set; }
        #endregion

        /// <summary>
        /// 初始化协议信息
        /// </summary>
        public static void InitManager()
        {
            ReadProtocolsFromServer();
        }

        /// <summary>
        /// 从服务器读取所有通信协议的信息
        /// </summary>
        /// <returns></returns>
        private static void ReadProtocolsFromServer()
        {
            var process = new ProtocolCodingProcess();

            Firmwares = process.GetAllFirmwares();
        }
    }
}
