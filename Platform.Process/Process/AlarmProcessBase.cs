﻿using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 报警信息处理基类
    /// </summary>
    public class AlarmProcessBase
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