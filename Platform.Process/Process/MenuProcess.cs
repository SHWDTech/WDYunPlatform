using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class MenuProcess : IMenuProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private MenuRepository DefaultRepository { get; } = new MenuRepository();

        public IEnumerable<IMenu> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IMenu> GetModels(Func<IMenu, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IMenu, bool> exp) => DefaultRepository.GetCount(exp);

        public IMenu CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IMenu ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IMenu model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IMenu> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IMenu> models) => DefaultRepository.Delete(models);

        public void Delete(IMenu model) => DefaultRepository.Delete(model);

        public bool IsExists(IMenu model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IMenu, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IMenu model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IMenu> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IMenu model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IMenu> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}