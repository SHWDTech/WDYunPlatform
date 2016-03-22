using System;
using System.Linq;
using System.Reflection;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding
{
    public static class PackageDeliver
    {
        private static readonly Type DeliverType;

        static PackageDeliver()
        {
            DeliverType = typeof (PackageDeliver);
        }

        /// <summary>
        /// 协议包处理程序
        /// </summary>
        /// <param name="package"></param>
        public static void Deliver(ProtocolPackage package)
        {
            foreach (var deliverMethod in package.DeliverParams.Split(';').Select(param => DeliverType.GetMethod(param, BindingFlags.Static)))
            {
                deliverMethod.Invoke(deliverMethod, new object[] {package});
            }
        }
    }
}
