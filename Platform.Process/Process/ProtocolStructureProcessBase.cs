using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 数据结构处理类
    /// </summary>
    public class ProtocolStructureProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private ProtocolStructureRepository DefaultRepository { get; } = new ProtocolStructureRepository();

        public IEnumerable<IProtocolStructure> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IProtocolStructure> GetModels(Func<IProtocolStructure, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IProtocolStructure, bool> exp) => DefaultRepository.GetCount(exp);

        public IProtocolStructure CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IProtocolStructure ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IProtocolStructure model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IProtocolStructure> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IProtocolStructure> models) => DefaultRepository.Delete(models);

        public void Delete(IProtocolStructure model) => DefaultRepository.Delete(model);

        public bool IsExists(IProtocolStructure model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IProtocolStructure, bool> exp) => DefaultRepository.IsExists(exp);

        public void MarkDelete(IProtocolStructure model) => DefaultRepository.MarkDelete(model);

        public void MarkDelete(IEnumerable<IProtocolStructure> models) => DefaultRepository.MarkDelete(models);

        public void SetEnableStatus(IProtocolStructure model, bool enableStatus)
            => DefaultRepository.SetEnableStatus(model, enableStatus);

        public void SetEnableStatus(IEnumerable<IProtocolStructure> models, bool enableStatus)
            => DefaultRepository.SetEnableStatus(models, enableStatus);
    }
}