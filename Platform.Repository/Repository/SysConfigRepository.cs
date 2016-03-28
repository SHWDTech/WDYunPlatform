using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统配置数据仓库
    /// </summary>
    public class SysConfigRepository : SysRepository<SysConfig>, ISysConfigRepository
    {
        public Dictionary<string, string> GetSysConfigDictionary(Func<SysConfig, bool> exp)
            => GetModels(exp).ToDictionary(config => config.SysConfigName, config => config.SysConfigValue);
    }
}