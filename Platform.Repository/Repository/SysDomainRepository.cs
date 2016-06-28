using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System.Linq;
using SHWD.Platform.Repository.Entities;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 带有域的系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysDomainRepository<T> : SysRepository<T>, ISysDomainRepository<T> where T : class, ISysDomainModel, new()
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysDomainRepository()
        {
            EntitySet = EntitySet.Where(model => model.DomainId == CurrentDomain.Id);
            CheckFunc = (obj => obj.DomainId == CurrentDomain.Id);
        }

        protected SysDomainRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// 创建默认数据模型
        /// </summary>
        /// <returns></returns>
        public new static T CreateDefaultModel()
        {
            var model = SysRepository<T>.CreateDefaultModel();
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