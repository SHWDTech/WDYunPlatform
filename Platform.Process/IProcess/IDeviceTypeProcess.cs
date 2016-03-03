using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 设备类型处理接口
    /// </summary>
    public interface IDeviceTypeTypeProcess
    {
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <returns>所有设备类型</returns>
        IEnumerable<IDeviceType> GetAllModels();

        /// <summary>
        /// 获取指定设备类型
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的设备类型</returns>
        IEnumerable<IDeviceType> GetModels(Func<IDeviceType, bool> exp);

        /// <summary>
        /// 获取符合条件的设备类型的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的设备类型的数量</returns>
        int GetCount(Func<IDeviceType, bool> exp);

        /// <summary>
        /// 新建默认数据设备类型
        /// </summary>
        /// <returns>默认数据设备类型</returns>
        IDeviceType CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析设备类型
        /// </summary>
        /// <param name="jsonString">包含设备类型信息的JSON字符串</param>
        /// <returns>解析后的设备类型</returns>
        IDeviceType ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改设备类型
        /// </summary>
        /// <param name="model">被添加或修改的设备类型</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IDeviceType model);

        /// <summary>
        /// 批量添加设备类型
        /// </summary>
        /// <param name="models">被添加或修改的设备类型列表</param>
        /// <returns>成功添加或修改的设备类型数量</returns>
        int AddOrUpdate(IEnumerable<IDeviceType> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IDeviceType model);

        /// <summary>
        /// 批量删除设备类型
        /// </summary>
        /// <param name="models">被删除的设备类型列表</param>
        /// <returns>成功删除的设备类型数量</returns>
        int Delete(IEnumerable<IDeviceType> models);

        /// <summary>
        /// 判断设备类型是否存在
        /// </summary>
        /// <param name="model">被判断的设备类型</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IDeviceType model);

        /// <summary>
        /// 判断设备类型是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IDeviceType, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的设备类型</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IDeviceType model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的设备类型数量</returns>
        void MarkDelete(IEnumerable<IDeviceType> models);

        /// <summary>
        /// 设置设备类型启用状态
        /// </summary>
        /// <param name="model">设置启用状态的设备类型</param>
        /// <param name="enableStatus">Ture设置设备类型状态为启用，False设置设备类型状态为未启用</param>
        void SetEnableStatus(IDeviceType model, bool enableStatus);

        /// <summary>
        /// 批量设置设备类型启用状态
        /// </summary>
        /// <param name="models">设置启用状态的设备类型列表</param>
        /// <param name="enableStatus">Ture设置设备类型状态为启用，False设置设备类型状态为未启用</param>
        void SetEnableStatus(IEnumerable<IDeviceType> models, bool enableStatus);
    }
}