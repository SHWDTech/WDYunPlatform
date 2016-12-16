using System;
using SHWDTech.Platform.ProtocolService.ProtocolEncoding.Generics;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    /// <summary>
    /// 协议校验工具
    /// </summary>
    public static class ProtocolChecker
    {
        static ProtocolChecker()
        {
            Convert = typeof(ProtocolChecker);
        }

        private static readonly Type Convert;

        /// <summary>
        /// 校验协议
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static bool CheckProtocol<T>(IProtocolPackage<T> package)
        {
            var convertMethod = Convert.GetMethod($"{package.Protocol.CheckType}Checker");

            convertMethod = convertMethod.MakeGenericMethod(typeof(T));

            return (bool)convertMethod.Invoke(convertMethod, new object[] { package });
        }

        /// <summary>
        /// CRC16校验器
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static bool Crc16Checker<T>(IProtocolPackage<T> package)
        {
            var realpackage = package as IProtocolPackage<byte[]>;
            if (realpackage == null) return false;
            var calcCrc = Globals.GetUsmbcrc16(realpackage.GetBytes(), (ushort)(package.PackageLenth - 3));

            var protocolCrc = Globals.BytesToUint16(realpackage[Properties.Resource.CRCValue].ComponentContent, 0, false);

            return calcCrc == protocolCrc;
        }

        public static bool CrcModBusChecker<T>(IProtocolPackage<T> package)
        {
            var realpackage = package as IProtocolPackage<string>;
            if (realpackage == null) return false;
            var protocolBytes = realpackage.GetBytes();
            var calcCrc = Globals.GetCrcModBus(protocolBytes.SubBytes(6, protocolBytes.Length - 6));

            var protocolCrc = realpackage[Properties.Resource.CrcModBus].ComponentContent;

            return calcCrc == protocolCrc;
        }
    }
}
