using System;
using SHWDTech.Platform.Model.IModel;

namespace DbRepository
{
    public interface IDbRepository<T> : IDisposable where T : class, IModel, new()
    {
    }
}
