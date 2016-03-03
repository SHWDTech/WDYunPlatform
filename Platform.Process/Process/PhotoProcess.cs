using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    internal class PhotoProcess : IPhotoProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private PhotoRepository DefaultRepository { get; } = new PhotoRepository();

        public IEnumerable<IPhoto> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IPhoto> GetModels(Func<IPhoto, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IPhoto, bool> exp) => DefaultRepository.GetCount(exp);

        public IPhoto CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IPhoto ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IPhoto model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IPhoto> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IPhoto> models) => DefaultRepository.Delete(models);

        public void Delete(IPhoto model) => DefaultRepository.Delete(model);

        public bool IsExists(IPhoto model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IPhoto, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IPhoto model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IPhoto> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IPhoto model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IPhoto> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}