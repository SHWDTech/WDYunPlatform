﻿using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 图片数据仓库
    /// </summary>
    public class PhotoRepository : SysDomainRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository()
        {
            
        }

        public PhotoRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}