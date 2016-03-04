using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 摄像头信息处理基类
    /// </summary>
    public class CameraProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private CameraRepository DefaultRepository { get; } = new CameraRepository();

        public IEnumerable<ICamera> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<ICamera> GetModels(Func<ICamera, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<ICamera, bool> exp) => DefaultRepository.GetCount(exp);

        public ICamera CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public ICamera ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(ICamera model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<ICamera> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<ICamera> models) => DefaultRepository.Delete(models);

        public void Delete(ICamera model) => DefaultRepository.Delete(model);

        public bool IsExists(ICamera model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<ICamera, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(ICamera model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<ICamera> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(ICamera model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<ICamera> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}