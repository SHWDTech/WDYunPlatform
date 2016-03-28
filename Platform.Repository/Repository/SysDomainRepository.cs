using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System.Linq;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 带有域的系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysDomainRepository<T> : SysRepository<T>, ISysDomainRepository<T> where T : class, ISysDomainModel
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysDomainRepository()
        {
            EntitySet = EntitySet.Where(model => model.DomainId == CurrentDomain.Id);
            CheckFunc = (obj => obj.DomainId == CurrentDomain.Id);
        }

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Domain = CurrentDomain;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.Domain = CurrentDomain;

            return model;
        }
    }
}