using System;
using SHWDTech.Platform.Model.IModel;

namespace DbRepository
{
    public class DbRepository<T> : IDbRepository<T> where T : class, IModel, new()
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
