using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 设备处理接口
    /// </summary>
    public interface IDeviceProcess
    {
        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <returns>所有设备</returns>
        IEnumerable<IDevice> GetAllModels();

        /// <summary>
        /// 获取指定设备
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的设备</returns>
        IEnumerable<IDevice> GetModels(Func<IDevice, bool> exp);

        /// <summary>
        /// 获取符合条件的设备的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的设备的数量</returns>
        int GetCount(Func<IDevice, bool> exp);

        /// <summary>
        /// 新建默认数据设备
        /// </summary>
        /// <returns>默认数据设备</returns>
        IDevice CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析设备
        /// </summary>
        /// <param name="jsonString">包含设备信息的JSON字符串</param>
        /// <returns>解析后的设备</returns>
        IDevice ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改设备
        /// </summary>
        /// <param name="model">被添加或修改的设备</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IDevice model);

        /// <summary>
        /// 批量添加设备
        /// </summary>
        /// <param name="models">被添加或修改的设备列表</param>
        /// <returns>成功添加或修改的设备数量</returns>
        int AddOrUpdate(IEnumerable<IDevice> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IDevice model);

        /// <summary>
        /// 批量删除设备
        /// </summary>
        /// <param name="models">被删除的设备列表</param>
        /// <returns>成功删除的设备数量</returns>
        int Delete(IEnumerable<IDevice> models);

        /// <summary>
        /// 判断设备是否存在
        /// </summary>
        /// <param name="model">被判断的设备</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IDevice model);

        /// <summary>
        /// 判断设备是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IDevice, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的设备</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IDevice model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的设备数量</returns>
        void MarkDelete(IEnumerable<IDevice> models);

        /// <summary>
        /// 设置设备启用状态
        /// </summary>
        /// <param name="model">设置启用状态的设备</param>
        /// <param name="enableStatus">Ture设置设备状态为启用，False设置设备状态为未启用</param>
        void SetEnableStatus(IDevice model, bool enableStatus);

        /// <summary>
        /// 批量设置设备启用状态
        /// </summary>
        /// <param name="models">设置启用状态的设备列表</param>
        /// <param name="enableStatus">Ture设置设备状态为启用，False设置设备状态为未启用</param>
        void SetEnableStatus(IEnumerable<IDevice> models, bool enableStatus);
    }
}