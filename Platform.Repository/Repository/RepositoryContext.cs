﻿using System.Collections.Generic;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库上下文信息
    /// </summary>
    public class RepositoryContext : IRepositoryContext
    {
        public IWdUser CurrentUser { get; set; }

        public IDomain CurrentDomain { get; set; }

        public Dictionary<string, object> UserContext { get; set; }
    }
}