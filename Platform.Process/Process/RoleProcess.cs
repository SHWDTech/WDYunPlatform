using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class RoleProcess : IRoleProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private RoleRepository DefaultRepository { get; } = new RoleRepository();

        public IEnumerable<IRole> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IRole> GetModels(Func<IRole, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IRole, bool> exp) => DefaultRepository.GetCount(exp);

        public IRole CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IRole ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IRole model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IRole> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IRole> models) => DefaultRepository.Delete(models);

        public void Delete(IRole model) => DefaultRepository.Delete(model);

        public bool IsExists(IRole model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IRole, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IRole model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IRole> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IRole model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IRole> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}