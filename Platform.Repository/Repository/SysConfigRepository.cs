using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统配置数据仓库
    /// </summary>
    public class SysConfigRepository : SysRepository<SysConfig>, ISysConfigRepository
    {
        public SysConfigRepository()
        {
            
        }

        public SysConfigRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public Dictionary<string, string> GetSysConfigDictionary(Expression<Func<SysConfig, bool>> exp)
            => GetModels(exp).ToDictionary(config => config.SysConfigName, config => config.SysConfigValue);
    }
}