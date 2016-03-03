using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    public class ProtocolProcess : IProtocolProcess
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private ProtocolRepository DefaultRepository { get; } = new ProtocolRepository();

        public IEnumerable<IProtocol> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IProtocol> GetModels(Func<IProtocol, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IProtocol, bool> exp) => DefaultRepository.GetCount(exp);

        public IProtocol CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IProtocol ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IProtocol model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IProtocol> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IProtocol> models) => DefaultRepository.Delete(models);

        public void Delete(IProtocol model) => DefaultRepository.Delete(model);

        public bool IsExists(IProtocol model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IProtocol, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IProtocol model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IProtocol> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IProtocol model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IProtocol> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}