using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface ICameraProcess
    {
        /// <summary>
        /// 获取所有摄像头
        /// </summary>
        /// <returns>所有摄像头</returns>
        IEnumerable<ICamera> GetAllModels();

        /// <summary>
        /// 获取指定摄像头
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的摄像头</returns>
        IEnumerable<ICamera> GetModels(Func<ICamera, bool> exp);

        /// <summary>
        /// 获取符合条件的摄像头的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的摄像头的数量</returns>
        int GetCount(Func<ICamera, bool> exp);

        /// <summary>
        /// 新建默认数据摄像头
        /// </summary>
        /// <returns>默认数据摄像头</returns>
        ICamera CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析摄像头
        /// </summary>
        /// <param name="jsonString">包含摄像头信息的JSON字符串</param>
        /// <returns>解析后的摄像头</returns>
        ICamera ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改摄像头
        /// </summary>
        /// <param name="model">被添加或修改的摄像头</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(ICamera model);

        /// <summary>
        /// 批量添加摄像头
        /// </summary>
        /// <param name="models">被添加或修改的摄像头列表</param>
        /// <returns>成功添加或修改的摄像头数量</returns>
        int AddOrUpdate(IEnumerable<ICamera> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(ICamera model);

        /// <summary>
        /// 批量删除摄像头
        /// </summary>
        /// <param name="models">被删除的摄像头列表</param>
        /// <returns>成功删除的摄像头数量</returns>
        int Delete(IEnumerable<ICamera> models);

        /// <summary>
        /// 判断摄像头是否存在
        /// </summary>
        /// <param name="model">被判断的摄像头</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(ICamera model);

        /// <summary>
        /// 判断摄像头是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<ICamera, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的摄像头</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(ICamera model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的摄像头数量</returns>
        void MarkDelete(IEnumerable<ICamera> models);

        /// <summary>
        /// 设置摄像头启用状态
        /// </summary>
        /// <param name="model">设置启用状态的摄像头</param>
        /// <param name="enableStatus">Ture设置摄像头状态为启用，False设置摄像头状态为未启用</param>
        void SetEnableStatus(ICamera model, bool enableStatus);

        /// <summary>
        /// 批量设置摄像头启用状态
        /// </summary>
        /// <param name="models">设置启用状态的摄像头列表</param>
        /// <param name="enableStatus">Ture设置摄像头状态为启用，False设置摄像头状态为未启用</param>
        void SetEnableStatus(IEnumerable<ICamera> models, bool enableStatus);
    }
}