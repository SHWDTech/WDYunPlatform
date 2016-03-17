using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议编解码器
    /// </summary>
    public class ProtocolCoder
    {
        /// <summary>
        /// 创建一个新的编解码器，并初始化编解码器对应的设备
        /// </summary>
        /// <param name="deviceGuid">对应的设备</param>
        public ProtocolCoder(Guid deviceGuid)
        {
            _protocols = ProtocolInfoManager.GetDeviceProtocolsFullLoaded(deviceGuid);
        }

        /// <summary>
        /// 设备对应的协议
        /// </summary>
        private readonly IList<Protocol> _protocols; 

        public void EncodeProtocol()
        {
            
        }


        public void DecodeProtocol(byte[] protocolBytes)
        {
            
        }
    }
}
