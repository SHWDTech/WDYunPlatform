using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class PermissionProcess : IPermissionProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private PermissionRepository DefaultRepository { get; } = new PermissionRepository();

        public IEnumerable<IPermission> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IPermission> GetModels(Func<IPermission, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IPermission, bool> exp) => DefaultRepository.GetCount(exp);

        public IPermission CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IPermission ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IPermission model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IPermission> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IPermission> models) => DefaultRepository.Delete(models);

        public void Delete(IPermission model) => DefaultRepository.Delete(model);

        public bool IsExists(IPermission model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IPermission, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IPermission model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IPermission> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IPermission model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IPermission> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}