using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 系统配置数据仓库接口
    /// </summary>
    public interface ISysConfigRepository : ISysRepository<SysConfig>
    {
        Dictionary<string, string> GetSysConfigDictionary(Expression<Func<SysConfig, bool>> exp);
    }
}