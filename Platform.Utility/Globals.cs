using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SHWDTech.Platform.Utility
{
    public class Globals
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
            identityNum |= (long)dt.Millisecond << 0;

            return identityNum;
        }
    }
}