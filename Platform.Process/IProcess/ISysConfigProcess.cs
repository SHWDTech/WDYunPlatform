using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 系统配置处理接口
    /// </summary>
    public interface ISysConfigProcess : IProcessBase
    {
        /// <summary>
        /// 获取指定类型的系统配置列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<SysConfig> GetSysConfigsByType(string type);
    }
}
