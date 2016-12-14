using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IModel, new()
    {
        private DbSet<T> _entitySet;

        private IQueryable<T> _entityQuery;

        public DbSet<T> EntitySet
        {
            get
            {
                if (_entitySet == null && DbContext == null) return null;
                return _entitySet ?? (_entitySet = DbContext.Set<T>());
            }

        }

        public IQueryable<T> EntityQuery
        {
            get
            {
                if (_entityQuery == null && DbContext == null) return null;
                return _entityQuery ?? (_entityQuery = _entitySet.AsQueryable());
            }
        }

        public Repository()
        {

        }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual int AddOrUpdate(IEnumerable<T> models, bool? commit = null)
        {
            foreach (var model in models)
            {
                EntitySet.AddOrUpdate(model);
            }

            TryCommit(commit);

            return 0;
        }

        public virtual T AddOrUpdate(T model, bool? commit = null)
        {
            EntitySet.AddOrUpdate(model);

            TryCommit(commit);

            return model;
        }

        public virtual int Commit()
        {
            return DbContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<T> models, bool? commit = null)
        {
            foreach (var model in models)
            {
                EntitySet.Remove(model);
            }

            TryCommit(commit);
        }

        public virtual void Delete(T model, bool? commit = null)
        {
            EntitySet.Remove(model);
            TryCommit(commit);
        }

        public virtual T First(Expression<Func<T, bool>> exp)
        {
            return EntitySet.FirstOrDefault(exp);
        }

        public virtual T FirstInclude(Expression<Func<T, bool>> exp, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => current.Include(include));

            return query.FirstOrDefault(exp);
        }

        public virtual IQueryable<T> FirstIncludeQueryable(Expression<Func<T, bool>> exp, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => (DbSet<T>)current.Include(include));

            return query.Where(exp);
        }

        public virtual IQueryable<T> FirstQueryable(Expression<Func<T, bool>> exp)
        {
            return EntitySet.Take(1);
        }

        public virtual bool IsExists(Func<T, bool> exp)
        {
            return EntitySet.Any(exp);
        }

        public virtual T ParseModel(string jsonString)
        {
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            return model;
        }

        public virtual IList<T> ParseModelList(string jsonString)
        {
            var models = JsonConvert.DeserializeObject<IList<T>>(jsonString);
            return models;
        }

        public virtual int PartialUpdate(IEnumerable<T> models, List<string> propertyNames, bool? commit = null)
        {
            foreach (var model in models)
            {
                EntitySet.Attach(model);
                var modelType = model.GetType();
                foreach (var propertyName in propertyNames)
                {
                    if (!IsPrimitive(modelType.GetProperty(propertyName).PropertyType)) continue;

                    DbContext.Entry(model).Property(propertyName).IsModified = true;
                }
            }

            return TryCommit(commit);
        }

        public virtual T PartialUpdate(T model, List<string> propertyNames, bool? commit = null)
        {
            EntitySet.Attach(model);
            var modelType = model.GetType();
            foreach (var propertyName in propertyNames)
            {
                if (!IsPrimitive(modelType.GetProperty(propertyName).PropertyType)) continue;

                DbContext.Entry(model).Property(propertyName).IsModified = true;
            }

            TryCommit(commit);

            return model;
        }

        public virtual IList<T> Where(Expression<Func<T, bool>> exp)
            => EntitySet.Where(exp).ToList();

        public virtual IList<T> WhereInclude(Expression<Func<T, bool>> exp, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => current.Include(include));

            return query.Where(exp).ToList();
        }

        public IQueryable<T> WhereIncludeQueryable(Expression<Func<T, bool>> exp, string[] includes)
        {
            var query = includes.Aggregate(EntityQuery, (current, include) => current.Include(include));

            return query.Where(exp);
        }

        public IQueryable<T> WhereQueryable(Expression<Func<T, bool>> exp)
            => EntitySet.Where(exp);

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive
            || type == typeof(decimal)
            || type == typeof(string)
            || type == typeof(DateTime)
            || type == typeof(Guid)
            || (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && type.GetGenericArguments().Any(t => t.IsValueType && IsPrimitive(t)));
        }

        private int TryCommit(bool? commit)
        {
            if (commit == null && AutoCommit)
            {
                return Commit();
            }
            return commit == true ? Commit() : 0;
        }
    }
}
