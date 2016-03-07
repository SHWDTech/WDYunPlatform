using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 角色信息处理基类
    /// </summary>
    public class RoleProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private RoleRepository DefaultRepository { get; } = new RoleRepository();

        public IEnumerable<IWdRole> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IWdRole> GetModels(Func<IWdRole, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IWdRole, bool> exp) => DefaultRepository.GetCount(exp);

        public IWdRole CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IWdRole ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IWdRole model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IWdRole> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IWdRole> models) => DefaultRepository.Delete(models);

        public void Delete(IWdRole model) => DefaultRepository.Delete(model);

        public bool IsExists(IWdRole model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IWdRole, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IWdRole model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IWdRole> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IWdRole model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IWdRole> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}