using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议数据包处理接口
    /// </summary>
    public interface IProtocolDataProcess
    {
        /// <summary>
        /// 获取所有协议数据包
        /// </summary>
        /// <returns>所有协议数据包</returns>
        IEnumerable<IProtocolData> GetAllModels();

        /// <summary>
        /// 获取指定协议数据包
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的协议数据包</returns>
        IEnumerable<IProtocolData> GetModels(Func<IProtocolData, bool> exp);

        /// <summary>
        /// 获取符合条件的协议数据包的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的协议数据包的数量</returns>
        int GetCount(Func<IProtocolData, bool> exp);

        /// <summary>
        /// 新建默认数据协议数据包
        /// </summary>
        /// <returns>默认数据协议数据包</returns>
        IProtocolData CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析协议数据包
        /// </summary>
        /// <param name="jsonString">包含协议数据包信息的JSON字符串</param>
        /// <returns>解析后的协议数据包</returns>
        IProtocolData ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改协议数据包
        /// </summary>
        /// <param name="model">被添加或修改的协议数据包</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IProtocolData model);

        /// <summary>
        /// 批量添加协议数据包
        /// </summary>
        /// <param name="models">被添加或修改的协议数据包列表</param>
        /// <returns>成功添加或修改的协议数据包数量</returns>
        int AddOrUpdate(IEnumerable<IProtocolData> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IProtocolData model);

        /// <summary>
        /// 批量删除协议数据包
        /// </summary>
        /// <param name="models">被删除的协议数据包列表</param>
        /// <returns>成功删除的协议数据包数量</returns>
        int Delete(IEnumerable<IProtocolData> models);

        /// <summary>
        /// 判断协议数据包是否存在
        /// </summary>
        /// <param name="model">被判断的协议数据包</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IProtocolData model);

        /// <summary>
        /// 判断协议数据包是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IProtocolData, bool> exp);
    }
}