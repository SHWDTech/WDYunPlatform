using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 系统自定义词典处理基类
    /// </summary>
    public class SysDictionaryProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private SysDictionaryRepository DefaultRepository { get; } = new SysDictionaryRepository();

        public IEnumerable<ISysDictionary> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<ISysDictionary> GetModels(Func<ISysDictionary, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<ISysDictionary, bool> exp) => DefaultRepository.GetCount(exp);

        public ISysDictionary CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public ISysDictionary ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(ISysDictionary model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<ISysDictionary> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<ISysDictionary> models) => DefaultRepository.Delete(models);

        public void Delete(ISysDictionary model) => DefaultRepository.Delete(model);

        public bool IsExists(ISysDictionary model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<ISysDictionary, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(ISysDictionary model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<ISysDictionary> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(ISysDictionary model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<ISysDictionary> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}