using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 带有域的系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysDomainRepository <T> : SysRepository<T>, ISysDomainRepository<T> where T: class, ISysDomainModel
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysDomainRepository()
        {
            
        } 

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Domain = ContextLocal.Value.CurrentDomain;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.Domain = ContextLocal.Value.CurrentDomain;

            return model;
        }

        public override Guid AddOrUpdate(T model)
        {
            if (model.Domain == null) model.Domain = ContextLocal.Value.CurrentDomain;

            return base.AddOrUpdate(model);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models.ToList();
            foreach (var model in enumerable.Where(model => model.Domain == null))
            {
                model.Domain = ContextLocal.Value.CurrentDomain;
            }

            return base.AddOrUpdate(enumerable);
        }
    }
}
