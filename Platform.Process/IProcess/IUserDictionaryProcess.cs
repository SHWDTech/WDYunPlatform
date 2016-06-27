namespace Platform.Process.IProcess
{
    /// <summary>
    /// 用户词典处理程序接口
    /// </summary>
    public interface IUserDictionaryProcess
    {
        /// <summary>
        /// 添加区域信息
        /// </summary>
        /// <param name="areaName">区域名称</param>
        /// <param name="areaLevel">区域层级</param>
        /// <param name="parentNode">父级区域</param>
        /// <returns></returns>
        object AddArea(string areaName, int areaLevel, string parentNode);

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <returns></returns>
        object GetAreaInfo();

        /// <summary>
        /// 删除区域信息
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        bool DeleteArea(string itemKey);
    }
}
