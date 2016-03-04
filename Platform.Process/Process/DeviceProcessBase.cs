using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 设备信息处理基类
    /// </summary>
    public class DeviceProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private DeviceRepository DefaultRepository { get; } = new DeviceRepository();

        public IEnumerable<IDevice> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IDevice> GetModels(Func<IDevice, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IDevice, bool> exp) => DefaultRepository.GetCount(exp);

        public IDevice CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IDevice ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IDevice model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IDevice> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IDevice> models) => DefaultRepository.Delete(models);

        public void Delete(IDevice model) => DefaultRepository.Delete(model);

        public bool IsExists(IDevice model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IDevice, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IDevice model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IDevice> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IDevice model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IDevice> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}