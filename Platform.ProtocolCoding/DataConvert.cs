using System;
using System.Reflection;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.Utility;

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
        /// <param name="packageComponent"></param>
        /// <returns></returns>
        public static object DecodeComponentData(IPackageComponent packageComponent)
        {
            var convertMethod = Convert.GetMethod($"{packageComponent.DataType}Decode", BindingFlags.Static);

            return convertMethod.Invoke(convertMethod, new object[] { packageComponent });
        }

        /// <summary>
        /// 编码组件数据
        /// </summary>
        /// <param name="packageComponent"></param>
        /// <param name="componentData"></param>
        /// <returns></returns>
        public static byte[] EncodeComponentData(IPackageComponent packageComponent, object componentData)
        {
            var convertMethod = Convert.GetMethod($"{packageComponent.DataType}Encode", BindingFlags.Static);

            return (byte[]) convertMethod.Invoke(convertMethod, new[] { componentData });
        }

        public static string NodeIdDecode(byte[] nodeIdBytes) => Globals.ByteArrayToHexString(nodeIdBytes);
    }
}
