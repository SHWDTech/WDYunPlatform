﻿using System;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 协议数据包数据仓库
    /// </summary>
    public class ProtocolDataRepository : DataRepository<ProtocolData>, IProtocolDataRepository
    {
        public ProtocolDataRepository()
        {
            
        }

        public ProtocolDataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public new static ProtocolData CreateDefaultModel()
        {
            var model = DataRepository<ProtocolData>.CreateDefaultModel();

            model.Id = Guid.Empty;

            return model;
        }
    }
}