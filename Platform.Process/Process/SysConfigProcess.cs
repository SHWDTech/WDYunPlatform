using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class SysConfigProcess : ISysConfigProcess
    {
        /// <summary>
        /// 系统配置数据仓库
        /// </summary>
        private readonly SysConfigRepository _sysConfigRepository = DbRepository.Repo<SysConfigRepository>();

        public IList<SysConfig> GetSysConfigsByType(string type)
            => _sysConfigRepository.GetModelList(config => config.SysConfigType == type);
    }
}
