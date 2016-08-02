using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Utility;

namespace DbRepository.Repository
{
    public class DbRepository<T> : IDbRepository<T> where T : class, IModel, new()
    {
        private readonly DbContext _dbContext;

        private IQueryable<T> _entitySet;

        public IQueryable<T> EntitySet 
            => _entitySet ?? (_entitySet = EntityCondition == null
                                        ? _dbContext.Set<T>()
                                        : _dbContext.Set<T>().Where(EntityCondition));

        public int Count => EntitySet.Count();

        public Expression<Func<T, bool>> EntityCondition { get; set; }

        private DbRepository()
        {
        }

        public DbRepository(string connString) : this()
        {
            _dbContext = new DbContext(connString);
        }

        public DbRepository(DbContext dbContext) : this()
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> GetAllModels()
            => EntitySet;

        public virtual IList<T> GetAllModelList()
            => EntitySet.ToList();

        public virtual IQueryable<T> GetModels(Expression<Func<T, bool>> exp)
            => EntitySet.Where(exp);

        public virtual T GetSingleModel(Expression<Func<T, bool>> exp)
            => EntitySet.SingleOrDefault(exp);

        public virtual T GetFirstModel(Expression<Func<T, bool>> exp)
            => EntitySet.FirstOrDefault(exp);

        public virtual IQueryable<T> GetModelsInclude(Expression<Func<T, bool>> exp, IList<string> includes)
            => includes.Aggregate(EntitySet, (current, include) => current.Include(include));

        public virtual T GetSingleModelInclude(Expression<Func<T, bool>> exp, IList<string> includes)
            => GetModelsInclude(exp, includes).SingleOrDefault();

        public virtual T GetFirstModelInclude(Expression<Func<T, bool>> exp, IList<string> includes)
            => GetModelsInclude(exp, includes).FirstOrDefault();

        public virtual T GetModelById(Guid guid)
            => EntitySet.SingleOrDefault(obj => obj.Id == guid);

        public virtual int GetCount(Expression<Func<T, bool>> exp)
            => EntitySet.Count(exp);

        public virtual T Create(bool autoGenerateId)
        {
            var model = _dbContext.Set<T>().Create();
            model.ModelState = ModelState.Added;

            if (autoGenerateId)
            {
                model.Id = Globals.NewCombId();
            }

            return model;
        }

        public virtual T ParseModelJson(string jsonString)
        {
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            return model;
        }

        public void AddOrUpdate(IEnumerable<T> models)
        {
            foreach (var model in models.Where(model => model.IsNew))
            {
                _dbContext.Set<T>().Add(model);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
