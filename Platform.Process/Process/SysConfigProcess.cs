using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class SysConfigProcess : ISysConfigProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private SysConfigRepository DefaultRepository { get; } = new SysConfigRepository();

        public IEnumerable<ISysConfig> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<ISysConfig> GetModels(Func<ISysConfig, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<ISysConfig, bool> exp) => DefaultRepository.GetCount(exp);

        public ISysConfig CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public ISysConfig ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(ISysConfig model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<ISysConfig> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<ISysConfig> models) => DefaultRepository.Delete(models);

        public void Delete(ISysConfig model) => DefaultRepository.Delete(model);

        public bool IsExists(ISysConfig model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<ISysConfig, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(ISysConfig model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<ISysConfig> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(ISysConfig model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<ISysConfig> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}