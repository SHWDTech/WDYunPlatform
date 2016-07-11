﻿using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据类型仓库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDataModel, new()
    {
        public DataRepository()
        {
            CheckFunc = (obj => obj.DomainId == CurrentDomain.Id);
        }

        public DataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            EntitySet = EntitySet.Where(model => model.DomainId == CurrentDomain.Id);
        }

        public new static T CreateDefaultModel(bool generateId = true)
        {
            var model = Repository<T>.CreateDefaultModel(generateId);

            model.DomainId = CurrentDomain.Id;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.DomainId = CurrentDomain.Id;

            return model;
        }
    }
}