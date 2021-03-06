﻿using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统自定义词典数据仓库
    /// </summary>
    public class SysDictionaryRepository : SysRepository<SysDictionary>, ISysDictionaryRepository
    {
        public SysDictionaryRepository()
        {
            
        }

        public SysDictionaryRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}