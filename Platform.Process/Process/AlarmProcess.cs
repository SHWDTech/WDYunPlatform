using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using Platform.Process.IProcess;

namespace Platform.Process.Process
{
    public class AlarmProcess : IAlarmProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private AlarmRepository DefaultRepository { get; } = new AlarmRepository();

        public IEnumerable<IAlarm> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IAlarm> GetModels(Func<IAlarm, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IAlarm, bool> exp) => DefaultRepository.GetCount(exp);

        public IAlarm CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IAlarm ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IAlarm model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IAlarm> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IAlarm> models) => DefaultRepository.Delete(models);

        public void Delete(IAlarm model) => DefaultRepository.Delete(model);

        public bool IsExists(IAlarm model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IAlarm, bool> exp) => DefaultRepository.IsExists(exp);
    }
}