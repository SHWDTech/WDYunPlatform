using System;
using System.Linq;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议包分发工具
    /// </summary>
    public static class PackageDeliver
    {
        /// <summary>
        /// 分发工具
        /// </summary>
        private static readonly Type Deliver;

        static PackageDeliver()
        {
            Deliver = typeof (PackageDeliver);
        }

        /// <summary>
        /// 协议包处理程序
        /// </summary>
        /// <param name="package"></param>
        /// <param name="source"></param>
        public static void Delive(IProtocolPackage package, IPackageSource source)
        {
            var deliverParams = package.DeliverParams.Split(';');

            foreach (var deliverMethod in deliverParams.Select(param => Deliver.GetMethod(param)))
            {
                deliverMethod.Invoke(deliverMethod, new object[] {package, source });
            }
        }

        public static void ReplyOriginal(IProtocolPackage package, IPackageSource source)
        {
            if (source.Type != PackageSourceType.CommunicationServer) return;

            source.Send(package.GetBytes());
        }
    }
}
