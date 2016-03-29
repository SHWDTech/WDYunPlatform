using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SHWDTech.Platform.Utility.Enum;

namespace SHWDTech.Platform.Utility
{
    public static class Globals
    {
        private static string _applicationPath = string.Empty;

        /// <summary>
        /// 返回应用程序启动路径
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_applicationPath))
                {
                    _applicationPath = AppDomain.CurrentDomain.BaseDirectory;

                    _applicationPath = _applicationPath.Replace("/", Path.DirectorySeparatorChar.ToString());

                    _applicationPath = _applicationPath.TrimEnd(Path.DirectorySeparatorChar);
                }

                return _applicationPath;
            }
        }

        /// <summary>
        /// 判断指定的Object是否是null、 全部是空格，活着不存在的值（DBNull）其中的一项
        /// </summary>
        /// <param name="obj">待检测的Object值</param>
        /// <returns>返回布尔类型，若参数是null、全部是空格，活着不存在的值（DBNull）中的一项，则返回True，否则返回False</returns>
        public static bool IsNullOrEmpty(object obj) =>
            obj == null || (obj is string && string.IsNullOrWhiteSpace(obj.ToString())) || obj is DBNull || (obj is Guid && (Guid)obj == Guid.Empty);

        /// <summary>
        /// 产生一个随机数字符串
        /// </summary>
        /// <param name="length">指定的字符串长度</param>
        /// <returns></returns>
        public static string Random(int length)
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());

            if (length <= 0) return rd.Next(2).ToString();

            var sb = new StringBuilder();
            for (var i = 0; i < 100; i++)
            {
                sb.Append(rd.Next(SafeInt("9".PadLeft(length, '9'))));

                if (sb.Length >= length)
                {
                    sb.Remove(length, sb.Length - length);
                    break;
                }
            }

            return sb.ToString();
        }

        public static int SafeInt(object obj)
            => SafeInt(obj, 0);

        public static int SafeInt(object obj, int defaultValue)
        {
            if (!IsNullOrEmpty(obj))
            {
                try
                {
                    return Convert.ToInt32(obj);
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取MD5加密字符串
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMd5(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str))).ToLower().Replace("-", "");
        }

        /// <summary>
        /// 生成22个字符的标识码
        /// </summary>
        /// <returns>标识码</returns>
        public static string NewIdentityCode()
        {
            Thread.Sleep(1);

            var identityNum = GetDateBytes(DateTime.Now);

            return identityNum.ToString("X2");
        }

        /// <summary>
        /// 根据传入的时间，计算一个long类型的值，存储时间信息（主要用来生成标识码）
        /// </summary>
        /// <param name="dt">传入的时间</param>
        /// <returns>一个Long</returns>
        public static long GetDateBytes(DateTime dt)
        {
            long identityNum = 0;

            //年取值0-9999， 12位 = 4098
            identityNum |= (long)dt.Year << 36;

            //月取值1-12， 4位 = 16
            identityNum |= (long)dt.Month << 32;

            //日取值0-31，5位 = 32
            identityNum |= (long)dt.Day << 27;

            //时取值0-23，5位 = 32
            identityNum |= (long)dt.Hour << 22;

            //分取值0-59，6位 = 64
            identityNum |= (long)dt.Minute << 16;

            //秒取值0-59，6位 = 64
            identityNum |= (long)dt.Second << 10;

            //毫秒取值0-999，10位 = 1024
            identityNum |= (long)dt.Millisecond << 0;

            return identityNum;
        }

        /// <summary>
        /// 返回位于指定起始位置和结束位置之间的字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="startIndex">返回字节集合的起始位置</param>
        /// <param name="endIndex">返回字节集合的结束位置</param>
        /// <returns>子字节数组</returns>
        public static byte[] SubBytes(this byte[] sourceBytes, int startIndex, int endIndex)
        {
            if (endIndex >= sourceBytes.Length) throw new ArgumentOutOfRangeException();

            if (startIndex >= endIndex) throw new ArgumentException("起始位置必须小于结束位置");

            var returnBytes = new byte[startIndex - endIndex];

            for (var i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = sourceBytes[startIndex + i];
            }

            return returnBytes;
        }

        /// <summary>
        /// 返回位于指定起始位置和结束位置之间的字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="startIndex">返回字节集合的起始位置</param>
        /// <param name="endIndex">返回字节集合的结束位置</param>
        /// <returns>子字节数组</returns>
        public static byte[] SubBytes(this IReadOnlyList<byte> sourceBytes, int startIndex, int endIndex)
        {
            if (endIndex >= sourceBytes.Count) throw new ArgumentOutOfRangeException();

            if (startIndex > endIndex) throw new ArgumentException("起始位置必须小于结束位置");

            if(startIndex == endIndex) return new byte[0];

            var returnBytes = new byte[endIndex - startIndex];

            for (var i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = sourceBytes[startIndex + i];
            }

            return returnBytes;
        }

        /// <summary>
        /// 判断风向
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static string DetectiveWindDirection(double angle)
        {
            if (angle < 0 || angle > 360) return WindDirection.OutOfRange;
            if (angle <= 11.25 || angle > 348.75) return WindDirection.North;
            if (angle > 11.25 && angle <= 33.75) return WindDirection.NorthNorthEast;
            if (angle > 33.75 && angle <= 56.25) return WindDirection.NorthEast;
            if (angle > 56.25 && angle <= 78.75) return WindDirection.EastNorthEast;
            if (angle > 78.75 && angle <= 101.25) return WindDirection.East;
            if (angle > 101.25 && angle <= 123.75) return WindDirection.EastSouthEast;
            if (angle > 123.75 && angle <= 146.25) return WindDirection.SouthEast;
            if (angle > 146.25 && angle <= 168.75) return WindDirection.SouthSouthEast;
            if (angle > 168.75 && angle <= 191.25) return WindDirection.South;
            if (angle > 191.25 && angle <= 213.75) return WindDirection.SouthSouthWest;
            if (angle > 213.75 && angle <= 236.25) return WindDirection.SouthWest;
            if (angle > 236.25 && angle <= 258.75) return WindDirection.WestSouthWest;
            if (angle > 258.75 && angle <= 281.25) return WindDirection.West;
            if (angle > 281.25 && angle <= 303.75) return WindDirection.WestNorthWest;
            if (angle > 303.75 && angle <= 326.25) return WindDirection.NorthWest;
            if (angle > 326.25 && angle <= 348.75) return WindDirection.NorthNorthWest;

            return WindDirection.UnKnow;
        }

        /// <summary>
        /// 判断风速
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string DetectiveWindSpeed(double speed, WindSpeedType type)
        {
            if (speed < 0) return WindSpeed.OutOfRange;

            if ((type == WindSpeedType.Hour && speed <= 1) ||
                (type == WindSpeedType.Second && speed <= 0.2)) return WindDirection.Const;

            if ((type == WindSpeedType.Hour && (speed > 1 || speed <= 5)) ||
                (type == WindSpeedType.Second && (speed > 0.2 || speed <= 1.5))) return WindSpeed.Soft;

            if ((type == WindSpeedType.Hour && (speed > 5 || speed <= 11)) ||
                (type == WindSpeedType.Second && (speed > 1.5 || speed <= 3.3))) return WindSpeed.Breeze;

            if ((type == WindSpeedType.Hour && (speed > 11 || speed <= 19)) ||
                (type == WindSpeedType.Second && (speed > 3.3 || speed <= 5.4))) return WindSpeed.GentleBreeze;

            if ((type == WindSpeedType.Hour && (speed > 19 || speed <= 28)) ||
                (type == WindSpeedType.Second && (speed > 5.4 || speed <= 7.9))) return WindSpeed.SoftBreeze;

            if ((type == WindSpeedType.Hour && (speed > 28 || speed <= 38)) ||
                (type == WindSpeedType.Second && (speed > 7.9 || speed <= 10.7))) return WindSpeed.CoolBreeze;

            if ((type == WindSpeedType.Hour && (speed > 38 || speed <= 49)) ||
                (type == WindSpeedType.Second && (speed > 10.7 || speed <= 13.8))) return WindSpeed.StrongBreeze;

            if ((type == WindSpeedType.Hour && (speed > 49 || speed <= 61)) ||
                (type == WindSpeedType.Second && (speed > 13.8 || speed <= 17.1))) return WindSpeed.StrongWind;

            if ((type == WindSpeedType.Hour && (speed > 61 || speed <= 74)) ||
                (type == WindSpeedType.Second && (speed > 17.1 || speed <= 20.7))) return WindSpeed.HighWind;

            if ((type == WindSpeedType.Hour && (speed > 74 || speed <= 88)) ||
                (type == WindSpeedType.Second && (speed > 20.7 || speed <= 24.4))) return WindSpeed.Gale;

            if ((type == WindSpeedType.Hour && (speed > 88 || speed <= 102)) ||
                (type == WindSpeedType.Second && (speed > 24.4 || speed <= 28.4))) return WindSpeed.StrongGale;

            if ((type == WindSpeedType.Hour && (speed > 102 || speed <= 117)) ||
                (type == WindSpeedType.Second && (speed > 28.4 || speed <= 32.6))) return WindSpeed.Storm;

            if ((type == WindSpeedType.Hour && (speed > 117)) ||
                (type == WindSpeedType.Second && (speed > 32.6))) return WindSpeed.Typhoon;

            return WindSpeed.UnKnow;
        }

        /// <summary>
        /// CRC校验数据表
        /// </summary>
        private static readonly ushort[] Crc1021Table = {
            0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
            0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
            0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6,
            0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
            0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485,
            0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
            0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4,
            0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
            0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823,
            0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
            0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12,
            0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
            0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41,
            0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
            0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70,
            0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
            0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f,
            0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
            0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e,
            0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
            0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d,
            0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
            0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c,
            0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
            0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab,
            0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
            0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
            0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
            0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9,
            0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
            0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8,
            0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
        };

        public static ushort GetUsmbcrc16(byte[] buffer, ushort usLen)
        {
            ushort crc16 = 0;

            for (var bufferIndex = 0; bufferIndex < usLen; bufferIndex++)
            {
                crc16 = (ushort)((crc16 << 8) ^ Crc1021Table[((crc16 >> 8) ^ buffer[bufferIndex]) & 0xFF]);
            }

            return crc16;
        }

        /// <summary>
        /// 将输入的Byte数组转换为十六进制显示的字符串
        /// </summary>
        /// <param name="data">需要转换的Byte数组</param>
        /// <returns>data的字符串表示形式</returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            var sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 将输入的Byte数组转换为UTF8字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToUtf8String(byte[] data) => Encoding.UTF8.GetString(data);

        /// <summary>
        /// 将输入的Byte数组转换为字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isHexMode"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] data, bool isHexMode) 
            => isHexMode ? ByteArrayToHexString(data) : ByteArrayToUtf8String(data);

        /// <summary>
        /// 将输入的字符串转换为Byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GbkStringToByteArray(string str) => Encoding.GetEncoding("GBK").GetBytes(str);

        /// <summary>
        /// 将输入的HEX字符串转换为Byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string str)
        {
            str = str.Replace(" ", "");
            if (str.Length%2 != 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            byte[] buffer = new byte[str.Length / 2];

            try
            {
                for (int i = 0; i < str.Length; i += 2)
                {
                    buffer[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
                }
            }
            catch (Exception)
            {
                return null;
            }
            
                
            return buffer;
        }

        /// <summary>
        /// 字符串转换为字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isHexMode"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string str, bool isHexMode)
            => isHexMode ? HexStringToByteArray(str) : GbkStringToByteArray(str);

        /// <summary>
        /// 获取本地IP地址的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIpAddressString()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var ipv4 = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();

            if(ipv4.Count == 0) return string.Empty;

            foreach (var ipAddress in ipv4)
            {
                var ipStr = ipAddress.ToString().Split('.');
                if (ipStr[0] == "192") return ipAddress.ToString();
            }

            return ipv4[0].ToString();
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var ipv4 = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();

            if (ipv4.Count == 0) return null;

            foreach (var ipAddress in ipv4)
            {
                var ipStr = ipAddress.ToString().Split('.');
                if (ipStr[0] == "192") return ipAddress;
            }

            return ipv4[0];
        }

        /// <summary>
        /// 获取一个端口号
        /// </summary>
        /// <returns></returns>
        public static int RandomPort()
        {
            var rd = new Random();
            return rd.Next(0, 65536);
        }
    }
}