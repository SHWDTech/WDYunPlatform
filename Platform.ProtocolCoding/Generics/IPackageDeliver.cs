using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding.Generics
{
    public interface IPackageDeliver<T>
    {
        /// <summary>
        /// 协议包处理程序
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        void Delive(IProtocolPackage<T> package, IPackageSource source);
    }
}
