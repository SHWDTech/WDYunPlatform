using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 监测数据处理基类
    /// </summary>
    public class MonitorDataProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private MonitorDataRepository DefaultRepository { get; } = new MonitorDataRepository();

        public IEnumerable<IMonitorData> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IMonitorData> GetModels(Func<IMonitorData, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IMonitorData, bool> exp) => DefaultRepository.GetCount(exp);

        public IMonitorData CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IMonitorData ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IMonitorData model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IMonitorData> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IMonitorData> models) => DefaultRepository.Delete(models);

        public void Delete(IMonitorData model) => DefaultRepository.Delete(model);

        public bool IsExists(IMonitorData model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IMonitorData, bool> exp) => DefaultRepository.IsExists(exp);
    }
}