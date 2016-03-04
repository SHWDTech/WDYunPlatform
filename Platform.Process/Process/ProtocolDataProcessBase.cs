using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.Process
{
    /// <summary>
    /// 协议数据包处理基类
    /// </summary>
    public class ProtocolDataProcessBase
    {
        /// <summary>
        /// 默认数据仓库
        /// </summary>
        private ProtocolDataRepository DefaultRepository { get; } = new ProtocolDataRepository();

        public IEnumerable<IProtocolData> GetAllModels() => DefaultRepository.GetAllModels();

        public IEnumerable<IProtocolData> GetModels(Func<IProtocolData, bool> exp) => DefaultRepository.GetModels(exp);

        public int GetCount(Func<IProtocolData, bool> exp) => DefaultRepository.GetCount(exp);

        public IProtocolData CreateDefaultModel() => DefaultRepository.CreateDefaultModel();

        public IProtocolData ParseModel(string jsonString) => DefaultRepository.ParseModel(jsonString);

        public Guid AddOrUpdate(IProtocolData model) => DefaultRepository.AddOrUpdate(model);

        public int AddOrUpdate(IEnumerable<IProtocolData> models) => DefaultRepository.AddOrUpdate(models);

        public int Delete(IEnumerable<IProtocolData> models) => DefaultRepository.Delete(models);

        public void Delete(IProtocolData model) => DefaultRepository.Delete(model);

        public bool IsExists(IProtocolData model) => DefaultRepository.IsExists(model);

        public bool IsExists(Func<IProtocolData, bool> exp) => DefaultRepository.IsExists(exp);
    }
}