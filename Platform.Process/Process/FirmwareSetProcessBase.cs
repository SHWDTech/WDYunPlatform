using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 固件集信息处理基类
    /// </summary>
    public class FirmwareSetProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private FirmwareSetRepository DefaultRepository { get; } = new FirmwareSetRepository();

        public IEnumerable<IFirmwareSet> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IFirmwareSet> GetModels(Func<IFirmwareSet, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IFirmwareSet, bool> exp) => DefaultRepository.GetCount(exp);

        public IFirmwareSet CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IFirmwareSet ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IFirmwareSet model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IFirmwareSet> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IFirmwareSet> models) => DefaultRepository.Delete(models);

        public void Delete(IFirmwareSet model) => DefaultRepository.Delete(model);

        public bool IsExists(IFirmwareSet model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IFirmwareSet, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IFirmwareSet model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IFirmwareSet> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IFirmwareSet model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IFirmwareSet> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}