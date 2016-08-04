using System;
using System.Collections.Generic;

namespace Platform.MessageQueue.Models
{
    /// <summary>
    /// 设备连接状态
    /// </summary>
    [Serializable]
    public class DeviceConnectingStatus
    {
        public DeviceConnectingStatus()
        {
            
        }

        public DeviceConnectingStatus(List<Guid> devices)
        {
            ConnectedDevices = devices;
        }

        public readonly List<Guid> ConnectedDevices;
    }
}
