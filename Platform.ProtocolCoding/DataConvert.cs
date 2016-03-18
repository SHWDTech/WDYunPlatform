using System;
using System.Reflection;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding
{
    public static class DataConvert
    {
        static DataConvert()
        {
            Convert = typeof (DataConvert);
        }

        private static readonly Type Convert;

        /// <summary>
        /// 解码组件数据
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static object DecodeComponentData(string dataType, Component component)
        {
            var convertMethod = Convert.GetMethod(dataType + "Convert", BindingFlags.Static);

            return convertMethod.Invoke(convertMethod, new object[] { component });
        }

        /// <summary>
        /// 编码组件数据
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="componentData"></param>
        /// <returns></returns>
        public static byte[] EncodeComponentData(string dataType, object componentData)
        {
            var convertMethod = Convert.GetMethod(dataType + "Encode", BindingFlags.Static);

            return (byte[])convertMethod.Invoke(convertMethod, new[] { componentData });
        }
    }
}
