using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.ProtocolService.DataBase;
using SHWDTech.Platform.ProtocolService.Provider;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public class EncodingManager
    {
        static EncodingManager()
        {
            ResolveClientSourceProvier();
        }

        /// <summary>
        /// 系统已经加载的所有协议信息
        /// </summary>
        private static readonly List<IProtocol> AllProtocols = new List<IProtocol>();

        private static readonly List<IClientSourceProvider> ClientSourceProviders = new List<IClientSourceProvider>();

        /// <summary>
        /// 系统已经加载的所有协议解码器类实例
        /// </summary>
        private static readonly Dictionary<string, IProtocolEncoder> ProtocolEncoders = new Dictionary<string, IProtocolEncoder>();

        public static AuthResult Authentication(byte[] authBytes)
        {
            var result = new AuthResult();
            var protocol = DetectProtocol(authBytes, AllProtocols);

            if (protocol == null)
            {
                return result;
            }

            var encoder = ProtocolEncoders[protocol.ProtocolModule];
            var package = encoder.Decode(authBytes, protocol);
            result.Package = package;
            if (!package.Finalized)
            {
                return result;
            }

            var clientSource = TryGetClientSource(package);
            if (clientSource == null)
            {
                result.ResultType = AuthenticationStatus.ClientNotRegistered;
                return result;
            }

            package.ClientSource = clientSource;
            return result;
        }

        /// <summary>
        /// 读取解码器类
        /// </summary>
        /// <param name="protocols"></param>
        public static void LoadEncoder(List<IProtocol> protocols)
        {
            foreach (var protocol in protocols)
            {
                try
                {
                    var encoder = UnityFactory.Resolve<IProtocolEncoder>(protocol.ProtocolModule);
                    AllProtocols.Add(protocol);
                    ProtocolEncoders.Add(protocol.ProtocolModule, encoder);
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error($"Load Encoder For Protocol {protocol.ProtocolModule} Failed.", ex);
                }
            }
        }

        private static IClientSource TryGetClientSource(IProtocolPackage package)
        {
            foreach (var clientSourceProvider in ClientSourceProviders)
            {
                var client = clientSourceProvider.GetClientSource(package.DeviceNodeId);
                if (client != null)
                {
                    return client;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取所有注册的客户端设备提供程序
        /// </summary>
        private static void ResolveClientSourceProvier()
        {
            try
            {
                var providers = UnityFactory.GetContainer().ResolveAll(typeof(IClientSourceProvider));
                foreach (var provider in providers)
                {
                    ClientSourceProviders.Add((IClientSourceProvider)provider);
                }
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("Load ClientSource Provider Failed.", ex);
            }
        }

        /// <summary>
        /// 检测数据对应的协议。
        /// </summary>
        /// <param name="bufferBytes"></param>
        /// <param name="protocols"></param>
        /// <returns></returns>
        private static IProtocol DetectProtocol(byte[] bufferBytes, List<IProtocol> protocols)
            => protocols.FirstOrDefault(obj => IsHeadMatched(bufferBytes, obj.Head));

        /// <summary>
        /// 协议帧头与字节流匹配
        /// </summary>
        /// <param name="bufferBytes">协议字节流</param>
        /// <param name="protocolHead">协议定义帧头</param>
        /// <returns>匹配返回TRUE，否则返回FALSE</returns>
        private static bool IsHeadMatched(byte[] bufferBytes, byte[] protocolHead)
        {
            if (bufferBytes.Length < protocolHead.Length)
            {
                return false;
            }

            return !protocolHead.Where((t, i) => bufferBytes[i] != t).Any();
        }
    }
}
