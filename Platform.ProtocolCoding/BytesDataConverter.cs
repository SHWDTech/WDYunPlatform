using System;
using System.Text;
using SHWDTech.Platform.ProtocolCoding.Generics;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding
{
    public class BytesDataConverter : DataConverter<byte[]>
    {
        public override object DecodeComponentData(IPackageComponent<byte[]> packageComponent)
        {
            var convertMethod = Converter.GetMethod($"{packageComponent.DataType}Decode");

            return convertMethod.Invoke(convertMethod, new object[] { packageComponent.ComponentContent });
        }

        public override byte[] EncodeComponentData(IPackageComponent<byte[]> packageComponent, object componentData)
        {
            var convertMethod = Converter.GetMethod($"{packageComponent.DataType}Encode");

            return (byte[])convertMethod.Invoke(convertMethod, new[] { componentData });
        }

        /// <summary>
        /// 解码NodeId
        /// </summary>
        /// <param name="nodeIdBytes"></param>
        /// <returns></returns>
        public static string NodeIdDecode(byte[] nodeIdBytes)
            => Globals.ByteArrayToHexString(nodeIdBytes, false).Trim();

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

        /// <summary>
        /// 解码存储在八个字节中的版本号信息
        /// </summary>
        /// <param name="versionBytes"></param>
        /// <returns></returns>
        public static string BytesToVersionNumberDecode(byte[] versionBytes)
            => $"Firm:{versionBytes[0]}.{versionBytes[1]}.{versionBytes[2]}.{versionBytes[3]}。" +
               $"Software:{versionBytes[4]}.{versionBytes[5]}.{versionBytes[6]}.{versionBytes[7]}。";

        /// <summary>
        /// 解码存储在16个字节中的GUID
        /// </summary>
        /// <param name="guidBytes"></param>
        /// <returns></returns>
        public static Guid BytesToGuidDecode(byte[] guidBytes)
            => new Guid(guidBytes);

        /// <summary>
        /// 解码存储在8个字节中的时间
        /// </summary>
        /// <param name="dateBytes"></param>
        /// <returns></returns>
        public static string BytesToDateStringDecode(byte[] dateBytes)
            => $"{2000 + dateBytes[0]}年{dateBytes[1]}月{dateBytes[2]}日{dateBytes[3]}时{dateBytes[4]}分{dateBytes[5]}秒";

        /// <summary>
        /// 解码存储在任意个字节中的GBK字符串
        /// </summary>
        /// <param name="gbkBytes"></param>
        /// <returns></returns>
        public static string BytesToGbkStringDecode(byte[] gbkBytes)
            => Encoding.GetEncoding("GBK").GetString(gbkBytes);

        /// <summary>
        /// 家吗存储在8个字节里的扩展NODEID
        /// </summary>
        /// <param name="nodeIdBytes"></param>
        /// <returns></returns>
        public static string BytesToExtendNodeIdDecode(byte[] nodeIdBytes)
            => $"ExtendNodeId:{Globals.ByteArrayToHexString(nodeIdBytes.SubBytes(0, 3), false).Trim()}。" +
               $"NodeId:{Globals.ByteArrayToHexString(nodeIdBytes.SubBytes(4, 7), false).Trim()}。";

        /// <summary>
        /// 解码存储在四个字节里的浮点数
        /// </summary>
        /// <param name="doubleBytes"></param>
        /// <returns></returns>
        public static double FourBytesToDoubleDecode(byte[] doubleBytes)
            => Globals.BytesToInt32(doubleBytes, 0, false) / 100.0;

        /// <summary>
        /// 解码存储在一个字节里的布尔值
        /// </summary>
        /// <param name="booleanBytes"></param>
        /// <returns></returns>
        public static bool ByteToBooleanDecode(byte[] booleanBytes)
            => booleanBytes[0] != 0;

        public static byte[] BytesToAlarmFlagDecode(byte[] flagBytes)
        {
            return null;
        }
    }
}
