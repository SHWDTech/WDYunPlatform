using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class DeviceTypeProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private DeviceTypeRepository DefaultRepository { get; } = new DeviceTypeRepository();

        public IEnumerable<IDeviceType> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IDeviceType> GetModels(Func<IDeviceType, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IDeviceType, bool> exp) => DefaultRepository.GetCount(exp);

        public IDeviceType CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IDeviceType ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IDeviceType model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IDeviceType> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IDeviceType> models) => DefaultRepository.Delete(models);

        public void Delete(IDeviceType model) => DefaultRepository.Delete(model);

        public bool IsExists(IDeviceType model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IDeviceType, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IDeviceType model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IDeviceType> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IDeviceType model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IDeviceType> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}