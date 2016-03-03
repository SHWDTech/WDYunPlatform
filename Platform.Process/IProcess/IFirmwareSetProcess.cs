using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 固件集处理接口
    /// </summary>
    public interface IFirmwareSetProcess
    {
        /// <summary>
        /// 获取所有固件集
        /// </summary>
        /// <returns>所有固件集</returns>
        IEnumerable<IFirmwareSet> GetAllModels();

        /// <summary>
        /// 获取指定固件集
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的固件集</returns>
        IEnumerable<IFirmwareSet> GetModels(Func<IFirmwareSet, bool> exp);

        /// <summary>
        /// 获取符合条件的固件集的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的固件集的数量</returns>
        int GetCount(Func<IFirmwareSet, bool> exp);

        /// <summary>
        /// 新建默认数据固件集
        /// </summary>
        /// <returns>默认数据固件集</returns>
        IFirmwareSet CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析固件集
        /// </summary>
        /// <param name="jsonString">包含固件集信息的JSON字符串</param>
        /// <returns>解析后的固件集</returns>
        IFirmwareSet ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改固件集
        /// </summary>
        /// <param name="model">被添加或修改的固件集</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IFirmwareSet model);

        /// <summary>
        /// 批量添加固件集
        /// </summary>
        /// <param name="models">被添加或修改的固件集列表</param>
        /// <returns>成功添加或修改的固件集数量</returns>
        int AddOrUpdate(IEnumerable<IFirmwareSet> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IFirmwareSet model);

        /// <summary>
        /// 批量删除固件集
        /// </summary>
        /// <param name="models">被删除的固件集列表</param>
        /// <returns>成功删除的固件集数量</returns>
        int Delete(IEnumerable<IFirmwareSet> models);

        /// <summary>
        /// 判断固件集是否存在
        /// </summary>
        /// <param name="model">被判断的固件集</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IFirmwareSet model);

        /// <summary>
        /// 判断固件集是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IFirmwareSet, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的固件集</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IFirmwareSet model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的固件集数量</returns>
        void MarkDelete(IEnumerable<IFirmwareSet> models);

        /// <summary>
        /// 设置固件集启用状态
        /// </summary>
        /// <param name="model">设置启用状态的固件集</param>
        /// <param name="enableStatus">Ture设置固件集状态为启用，False设置固件集状态为未启用</param>
        void SetEnableStatus(IFirmwareSet model, bool enableStatus);

        /// <summary>
        /// 批量设置固件集启用状态
        /// </summary>
        /// <param name="models">设置启用状态的固件集列表</param>
        /// <param name="enableStatus">Ture设置固件集状态为启用，False设置固件集状态为未启用</param>
        void SetEnableStatus(IEnumerable<IFirmwareSet> models, bool enableStatus);
    }
}