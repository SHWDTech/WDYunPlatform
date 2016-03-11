﻿using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统角色数据仓库
    /// </summary>
    public class RoleRepository : SysDomainRepository<WdRole>, IRoleRepository
    {
        public override WdRole CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Status = RoleStatus.Enabled;

            return model;
        }
    }
}