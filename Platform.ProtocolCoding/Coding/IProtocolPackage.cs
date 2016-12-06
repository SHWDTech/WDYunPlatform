using System.Collections.Generic;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 表示协议包
    /// </summary>
    public interface IProtocolPackage<T> : IProtocolPackage
    {
        /// <summary>
        /// 获取指定名称的数据段
        /// </summary>
        /// <param name="name">数据段名称</param>
        /// <returns>指定名称的数据段</returns>
        IPackageComponent<T> this[string name] { get; set; }

        /// <summary>
        /// 协议数据组件字典
        /// </summary>
        Dictionary<string, IPackageComponent<T>> DataComponents { get; }

        /// <summary>
        /// 添加数据段数据
        /// </summary>
        /// <param name="component"></param>
        void AppendData(IPackageComponent<T> component);
    }
}
