using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class UserConfigProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private UserConfigRepository DefaultRepository { get; } = new UserConfigRepository();

        public IEnumerable<IUserConfig> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IUserConfig> GetModels(Func<IUserConfig, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IUserConfig, bool> exp) => DefaultRepository.GetCount(exp);

        public IUserConfig CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IUserConfig ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IUserConfig model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IUserConfig> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IUserConfig> models) => DefaultRepository.Delete(models);

        public void Delete(IUserConfig model) => DefaultRepository.Delete(model);

        public bool IsExists(IUserConfig model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IUserConfig, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IUserConfig model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IUserConfig> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IUserConfig model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IUserConfig> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}