using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 报警信息处理接口
    /// </summary>
    public interface IAlarmProcess
    {
        /// <summary>
        /// 获取所有报警信息
        /// </summary>
        /// <returns>所有报警信息</returns>
        IEnumerable<IAlarm> GetAllModels();

        /// <summary>
        /// 获取指定报警信息
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的报警信息</returns>
        IEnumerable<IAlarm> GetModels(Func<IAlarm, bool> exp);

        /// <summary>
        /// 获取符合条件的报警信息的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的报警信息的数量</returns>
        int GetCount(Func<IAlarm, bool> exp);

        /// <summary>
        /// 新建默认数据报警信息
        /// </summary>
        /// <returns>默认数据报警信息</returns>
        IAlarm CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析报警信息
        /// </summary>
        /// <param name="jsonString">包含报警信息信息的JSON字符串</param>
        /// <returns>解析后的报警信息</returns>
        IAlarm ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改报警信息
        /// </summary>
        /// <param name="model">被添加或修改的报警信息</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IAlarm model);

        /// <summary>
        /// 批量添加报警信息
        /// </summary>
        /// <param name="models">被添加或修改的报警信息列表</param>
        /// <returns>成功添加或修改的报警信息数量</returns>
        int AddOrUpdate(IEnumerable<IAlarm> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IAlarm model);

        /// <summary>
        /// 批量删除报警信息
        /// </summary>
        /// <param name="models">被删除的报警信息列表</param>
        /// <returns>成功删除的报警信息数量</returns>
        int Delete(IEnumerable<IAlarm> models);

        /// <summary>
        /// 判断报警信息是否存在
        /// </summary>
        /// <param name="model">被判断的报警信息</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IAlarm model);

        /// <summary>
        /// 判断报警信息是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IAlarm, bool> exp);
    }
}