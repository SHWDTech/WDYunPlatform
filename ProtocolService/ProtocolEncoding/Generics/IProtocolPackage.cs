using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics
{
    public interface IProtocolPackage<T> : IProtocolPackage
    {
        /// <summary>
        /// 获取指定名称的数据段
        /// </summary>
        /// <param name="name">数据段名称</param>
        /// <returns>指定名称的数据段</returns>
        IPackageComponent<T> this[string name] { get; set; }

        /// <summary>
        /// 数据段协议包组件
        /// </summary>
        IPackageComponent<T> DataComponent { get; }

        /// <summary>
        /// 协议数据组件字典
        /// </summary>
        Dictionary<string, IPackageComponent<T>> DataComponents { get; }

        /// <summary>
        /// 添加数据段数据
        /// </summary>
        /// <param name="component"></param>
        void AppendData(IPackageComponent<T> component);

        /// <summary>
        /// 获取指定名称数据的字符串值
        /// </summary>
        /// <param name="dataValueName"></param>
        /// <returns></returns>
        string GetDataValueString(string dataValueName);

        /// <summary>
        /// 获取需要计算的CRC数据段
        /// </summary>
        /// <returns></returns>
        byte[] GetCrcBytes();
    }
}
