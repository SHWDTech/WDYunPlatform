using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class UserDictionaryProcess : IUserDictionaryDictionaryProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private UserDictionaryRepository DefaultRepository { get; } = new UserDictionaryRepository();

        public IEnumerable<IUserDictionary> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IUserDictionary> GetModels(Func<IUserDictionary, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IUserDictionary, bool> exp) => DefaultRepository.GetCount(exp);

        public IUserDictionary CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IUserDictionary ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IUserDictionary model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IUserDictionary> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IUserDictionary> models) => DefaultRepository.Delete(models);

        public void Delete(IUserDictionary model) => DefaultRepository.Delete(model);

        public bool IsExists(IUserDictionary model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IUserDictionary, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IUserDictionary model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IUserDictionary> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IUserDictionary model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IUserDictionary> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}