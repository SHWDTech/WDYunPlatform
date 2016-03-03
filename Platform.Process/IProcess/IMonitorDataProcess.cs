using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface IMonitorDataProcess
    {
        /// <summary>
        /// 获取所有检测数据
        /// </summary>
        /// <returns>所有检测数据</returns>
        IEnumerable<IMonitorData> GetAllModels();

        /// <summary>
        /// 获取指定检测数据
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的检测数据</returns>
        IEnumerable<IMonitorData> GetModels(Func<IMonitorData, bool> exp);

        /// <summary>
        /// 获取符合条件的检测数据的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的检测数据的数量</returns>
        int GetCount(Func<IMonitorData, bool> exp);

        /// <summary>
        /// 新建默认数据检测数据
        /// </summary>
        /// <returns>默认数据检测数据</returns>
        IMonitorData CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析检测数据
        /// </summary>
        /// <param name="jsonString">包含检测数据信息的JSON字符串</param>
        /// <returns>解析后的检测数据</returns>
        IMonitorData ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改检测数据
        /// </summary>
        /// <param name="model">被添加或修改的检测数据</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IMonitorData model);

        /// <summary>
        /// 批量添加检测数据
        /// </summary>
        /// <param name="models">被添加或修改的检测数据列表</param>
        /// <returns>成功添加或修改的检测数据数量</returns>
        int AddOrUpdate(IEnumerable<IMonitorData> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IMonitorData model);

        /// <summary>
        /// 批量删除检测数据
        /// </summary>
        /// <param name="models">被删除的检测数据列表</param>
        /// <returns>成功删除的检测数据数量</returns>
        int Delete(IEnumerable<IMonitorData> models);

        /// <summary>
        /// 判断检测数据是否存在
        /// </summary>
        /// <param name="model">被判断的检测数据</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IMonitorData model);

        /// <summary>
        /// 判断检测数据是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IMonitorData, bool> exp);
    }
}