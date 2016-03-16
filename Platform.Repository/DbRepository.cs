using SHWD.Platform.Repository.Repository;

namespace SHWD.Platform.Repository
{
    public static class DbRepository
    {
        public static T Repo<T>() where T : RepositoryBase, new() => new T();
    }
}
