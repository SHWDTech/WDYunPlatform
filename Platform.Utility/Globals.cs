using System;
using System.IO;
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
            identityNum |= (long) dt.Year << 36;

            //月取值1-12， 4位 = 16
            identityNum |= (long) dt.Month << 32;

            //日取值0-31，5位 = 32
            identityNum |= (long) dt.Day << 27;

            //时取值0-23，5位 = 32
            identityNum |= (long) dt.Hour << 22;

            //分取值0-59，6位 = 64
            identityNum |= (long) dt.Minute << 16;

            //秒取值0-59，6位 = 64
            identityNum |= (long) dt.Second << 10;

            //毫秒取值0-999，10位 = 1024
            identityNum |= (long) dt.Millisecond << 0;

            return identityNum;
        }

        /// <summary>
        /// 返回位于指定起始位置和结束位置之间的字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="startIndex">返回字节集合的起始位置</param>
        /// <param name="endIndex">返回字节集合的结束位置</param>
        /// <returns></returns>
        public static byte[] SubBytes(this byte[] sourceBytes, int startIndex, int endIndex)
        {
            if (endIndex >= sourceBytes.Length) throw new ArgumentOutOfRangeException();

            if(startIndex >= endIndex) throw new ArgumentException("起始位置必须小于结束位置");

            var returnBytes = new byte[startIndex - endIndex];

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

            if ((type == WindSpeedType.Hour && (speed  > 1 || speed <= 5)) ||
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
                (type == WindSpeedType.Second && (speed > 17.1 || speed <=20.7))) return WindSpeed.HighWind;

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
    }
}