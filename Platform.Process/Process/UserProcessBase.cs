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

        public IEnumerable<IUser> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IUser> GetModels(Func<IUser, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IUser, bool> exp) => DefaultRepository.GetCount(exp);

        public IUser CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IUser ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IUser model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IUser> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IUser> models) => DefaultRepository.Delete(models);

        public void Delete(IUser model) => DefaultRepository.Delete(model);

        public bool IsExists(IUser model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IUser, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IUser model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IUser> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IUser model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IUser> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}