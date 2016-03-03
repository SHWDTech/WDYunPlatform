using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class TaskProcess : ITaskProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private TaskRepository DefaultRepository { get; } = new TaskRepository();

        public IEnumerable<ITask> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<ITask> GetModels(Func<ITask, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<ITask, bool> exp) => DefaultRepository.GetCount(exp);

        public ITask CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public ITask ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(ITask model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<ITask> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<ITask> models) => DefaultRepository.Delete(models);

        public void Delete(ITask model) => DefaultRepository.Delete(model);

        public bool IsExists(ITask model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<ITask, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(ITask model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<ITask> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(ITask model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<ITask> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}