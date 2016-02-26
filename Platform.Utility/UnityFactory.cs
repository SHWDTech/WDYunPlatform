using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace SHWDTech.Platform.Utility
{
    public static class UnityFactory
    {
        private static IUnityContainer _container;

        /// <summary>
        /// 获取Unity Container
        /// </summary>
        /// <returns>全局唯一的Unity Container实例</returns>
        public static IUnityContainer GetContainer()
        {
            if (null == _container)
            {
                _container = new UnityContainer();

                string appPath = Globals.ApplicationPath;

                var fileMap = new ExeConfigurationFileMap
                                    {
                                        ExeConfigFilename = appPath + "\\" + "Unity.Config"
                                    };

                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap,
                    ConfigurationUserLevel.None);

                var unitySection = (UnityConfigurationSection) configuration.GetSection("unity");

                if (unitySection != null)
                {
                    _container.LoadConfiguration(unitySection);
                }
            }

            return _container;
        }

        public static T Resolve<T>() => GetContainer().Resolve<T>();

        public static T Resolve<T>(string name) => GetContainer().Resolve<T>(name);

        public static void ClearContainer()
        {
            if (_container == null) return;
            _container.Dispose();
            _container = null;
        }
    }
}
