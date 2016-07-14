using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface IDeviceModelProcess : IProcessBase
    {
        /// <summary>
        /// 获取设备型号选单列表
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetDeviceModelSelectList();
    }
}
