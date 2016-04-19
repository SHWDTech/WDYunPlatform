using System;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding
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
        public static bool CheckProtocol(IProtocolPackage package)
        {
            var convertMethod = Convert.GetMethod($"{package.Protocol.CheckType}Checker");

            return (bool)convertMethod.Invoke(convertMethod, new object[] { package });
        }

        /// <summary>
        /// CRC16校验器
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static bool Crc16Checker(IProtocolPackage package)
        {
            var calcCrc = Globals.GetUsmbcrc16(package.GetBytes(), (ushort)(package.PackageLenth - 3));

            var protocolCrc = Globals.BytesToUint16(package[StructureNames.CrcValue].ComponentBytes, 0, false);

            return calcCrc == protocolCrc;
        }
    }
}
