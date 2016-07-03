using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 系统配置处理类
    /// </summary>
    public class SysConfigProcess : ProcessBase, ISysConfigProcess
    {
        public IList<SysConfig> GetSysConfigsByType(string type)
            => Repo<SysConfigRepository>().GetModelList(config => config.SysConfigType == type);
    }
}
