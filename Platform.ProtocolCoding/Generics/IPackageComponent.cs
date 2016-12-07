using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    public interface IPackageComponent<T> : IPackageComponent
    {
        /// <summary>
        /// 组件数据
        /// </summary>
        T ComponentContent { get; set; }

        /// <summary>
        /// 组件数据值
        /// </summary>
        T ComponentValue { get; set; }
    }
}
