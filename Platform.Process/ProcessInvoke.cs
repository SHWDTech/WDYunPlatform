﻿using System.Threading;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process
{
    /// <summary>
    /// Process调用类
    /// </summary>
    public class ProcessInvoke
    {
        /// <summary>
        /// 设置仓库类Context
        /// </summary>
        /// <param name="context"></param>
        public static void SetupRepositoryContext(ThreadLocal<IRepositoryContext> context)
        {
            RepositoryBase.ContextLocal = context;
        }

        /// <summary>
        /// 注销仓库泪Context资源
        /// </summary>
        public static void DisposeRepositoryContext()
        {
            RepositoryBase.ContextLocal.Dispose();
        }
    }
}