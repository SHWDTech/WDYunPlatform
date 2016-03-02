﻿using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 项目数据仓库
    /// </summary>
    internal class ProjectRepository : SysDomainRepository<IProject>, IProjectRepository
    {
    }
}
