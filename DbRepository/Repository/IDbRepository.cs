using System;
using SHWDTech.Platform.Model.IModel;

namespace DbRepository.Repository
{
    public interface IDbRepository<T> : IDisposable where T : class, IModel, new()
    {
    }
}
