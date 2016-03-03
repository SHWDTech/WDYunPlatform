using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class FirmwareProcess : IFirmwareProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private FirmwareRepository DefaultRepository { get; } = new FirmwareRepository();

        public IEnumerable<IFirmware> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IFirmware> GetModels(Func<IFirmware, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IFirmware, bool> exp) => DefaultRepository.GetCount(exp);

        public IFirmware CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IFirmware ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IFirmware model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IFirmware> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IFirmware> models) => DefaultRepository.Delete(models);

        public void Delete(IFirmware model) => DefaultRepository.Delete(model);

        public bool IsExists(IFirmware model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IFirmware, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IFirmware model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IFirmware> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IFirmware model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IFirmware> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}