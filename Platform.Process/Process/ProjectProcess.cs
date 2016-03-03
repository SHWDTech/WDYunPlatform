using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class ProjectProcess : IProjectProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private ProjectRepository DefaultRepository { get; } = new ProjectRepository();

        public IEnumerable<IProject> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IProject> GetModels(Func<IProject, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IProject, bool> exp) => DefaultRepository.GetCount(exp);

        public IProject CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IProject ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IProject model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IProject> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IProject> models) => DefaultRepository.Delete(models);

        public void Delete(IProject model) => DefaultRepository.Delete(model);

        public bool IsExists(IProject model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IProject, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IProject model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IProject> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IProject model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IProject> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}