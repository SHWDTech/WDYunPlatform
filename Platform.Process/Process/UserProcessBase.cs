using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 用户信息处理基类
    /// </summary>
    public class UserProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private UserRepository DefaultRepository { get; } = new UserRepository();

        public IEnumerable<IWdUser> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IWdUser> GetModels(Func<IWdUser, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IWdUser, bool> exp) => DefaultRepository.GetCount(exp);

        public IWdUser CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IWdUser ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IWdUser model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IWdUser> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IWdUser> models) => DefaultRepository.Delete(models);

        public void Delete(IWdUser model) => DefaultRepository.Delete(model);

        public bool IsExists(IWdUser model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IWdUser, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IWdUser model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IWdUser> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IWdUser model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IWdUser> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}