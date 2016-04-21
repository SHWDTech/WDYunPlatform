using System;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.Utility;
// ReSharper disable UnusedMember.Local

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议数据段解码工具
    /// </summary>
    public static class DataConvert
    {
        static DataConvert()
        {
            Convert = typeof(DataConvert);
        }

        private static readonly Type Convert;

        /// <summary>
        /// 解码组件数据
        /// </summary>
        /// <param name="packageComponent"></param>
        /// <returns></returns>
        public static object DecodeComponentData(IPackageComponent packageComponent)
        {
            var convertMethod = Convert.GetMethod($"{packageComponent.DataType}Decode");

            return convertMethod.Invoke(convertMethod, new object[] { packageComponent.ComponentBytes });
        }

        /// <summary>
        /// 编码组件数据
        /// </summary>
        /// <param name="packageComponent"></param>
        /// <param name="componentData"></param>
        /// <returns></returns>
        public static byte[] EncodeComponentData(IPackageComponent packageComponent, object componentData)
        {
            var convertMethod = Convert.GetMethod($"{packageComponent.DataType}Encode");

            return (byte[])convertMethod.Invoke(convertMethod, new[] { componentData });
        }

        /// <summary>
        /// 解码NodeId
        /// </summary>
        /// <param name="nodeIdBytes"></param>
        /// <returns></returns>
        public static string NodeIdDecode(byte[] nodeIdBytes) => Globals.ByteArrayToHexString(nodeIdBytes, false).Trim();

        /// <summary>
        /// 解码数据有效性验证位
        /// </summary>
        /// <param name="flagBytes"></param>
        /// <returns></returns>
        public static IDataVallidFlag GetDataValidFlag(byte[] flagBytes)
        {
            var flagLength = flagBytes.Length * 8;

            var flag = new DataValidFlag(flagLength);

            var index = 0;
            foreach (var t in flagBytes)
            {
                for (var j = 0; j < 8; j++)
                {
                    flag.AddFlag(index, ((t >> j) & 0x01) == 1);
                    index++;
                }
            }

            return flag;
        }

        /// <summary>
        /// 解码两个字节存储的无符号整型
        /// </summary>
        /// <param name="uintBytes"></param>
        /// <returns></returns>
        public static uint FourBytesToUInt32Decode(byte[] uintBytes)
            => Globals.BytesToUint32(uintBytes, 0, false);

        /// <summary>
        /// 解码两个字节存储的浮点数，整数和小数部分分别储存
        /// </summary>
        /// <param name="doubleBytes"></param>
        /// <returns></returns>
        public static double TwoBytesToDoubleSeparateDecode(byte[] doubleBytes)
        {
            var intPart = doubleBytes[0];

            var decimalPart = doubleBytes[1] / 100.0;

            return intPart + decimalPart;
        }

        /// <summary>
        /// 解码两个字节存储的无符号短整型
        /// </summary>
        /// <param name="ushortBytes"></param>
        /// <returns></returns>
        public static ushort TwoBytesToUShortDecode(byte[] ushortBytes)
            => Globals.BytesToUint16(ushortBytes, 0, false);

        /// <summary>
        /// 解码两个字节存储的无符号短整型
        /// </summary>
        /// <param name="ushortBytes"></param>
        /// <returns></returns>
        public static ushort ThreeBytesToUShortDecode(byte[] ushortBytes)
            => Globals.BytesToUint16(ushortBytes, 1, false);

        /// <summary>
        /// 解码两个字节存储的浮点数，整数和小数部分统一储存
        /// </summary>
        /// <param name="doubleBytes"></param>
        /// <returns></returns>
        public static double TwoBytesToDoubleMergeDecode(byte[] doubleBytes)
            => Globals.BytesToUint16(doubleBytes, 0, false) / 10.0;

        /// <summary>
        /// 解码存储在四个字节中的两个无符号短整型
        /// </summary>
        /// <param name="intsBytes"></param>
        /// <returns></returns>
        public static ushort[] FourBytesToTwoUShortSeparateDecode(byte[] intsBytes)
        {
            var intArray = new ushort[2];

            intArray[0] = Globals.BytesToUint16(intsBytes, 0, false);

            intArray[1] = Globals.BytesToUint16(intsBytes, 2, false);

            return intArray;
        }
    }
}
