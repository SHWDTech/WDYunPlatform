using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 固件处理接口
    /// </summary>
    public interface IFirmwareProcess
    {
        /// <summary>
        /// 获取所有固件
        /// </summary>
        /// <returns>所有固件</returns>
        IEnumerable<IFirmware> GetAllModels();

        /// <summary>
        /// 获取指定固件
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的固件</returns>
        IEnumerable<IFirmware> GetModels(Func<IFirmware, bool> exp);

        /// <summary>
        /// 获取符合条件的固件的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的固件的数量</returns>
        int GetCount(Func<IFirmware, bool> exp);

        /// <summary>
        /// 新建默认数据固件
        /// </summary>
        /// <returns>默认数据固件</returns>
        IFirmware CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析固件
        /// </summary>
        /// <param name="jsonString">包含固件信息的JSON字符串</param>
        /// <returns>解析后的固件</returns>
        IFirmware ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改固件
        /// </summary>
        /// <param name="model">被添加或修改的固件</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IFirmware model);

        /// <summary>
        /// 批量添加固件
        /// </summary>
        /// <param name="models">被添加或修改的固件列表</param>
        /// <returns>成功添加或修改的固件数量</returns>
        int AddOrUpdate(IEnumerable<IFirmware> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IFirmware model);

        /// <summary>
        /// 批量删除固件
        /// </summary>
        /// <param name="models">被删除的固件列表</param>
        /// <returns>成功删除的固件数量</returns>
        int Delete(IEnumerable<IFirmware> models);

        /// <summary>
        /// 判断固件是否存在
        /// </summary>
        /// <param name="model">被判断的固件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IFirmware model);

        /// <summary>
        /// 判断固件是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IFirmware, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的固件</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IFirmware model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的固件数量</returns>
        void MarkDelete(IEnumerable<IFirmware> models);

        /// <summary>
        /// 设置固件启用状态
        /// </summary>
        /// <param name="model">设置启用状态的固件</param>
        /// <param name="enableStatus">Ture设置固件状态为启用，False设置固件状态为未启用</param>
        void SetEnableStatus(IFirmware model, bool enableStatus);

        /// <summary>
        /// 批量设置固件启用状态
        /// </summary>
        /// <param name="models">设置启用状态的固件列表</param>
        /// <param name="enableStatus">Ture设置固件状态为启用，False设置固件状态为未启用</param>
        void SetEnableStatus(IEnumerable<IFirmware> models, bool enableStatus);
    }
}