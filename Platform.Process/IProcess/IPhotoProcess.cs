using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 图片处理接口
    /// </summary>
    public interface IPhotoProcess
    {
        /// <summary>
        /// 获取所有图片
        /// </summary>
        /// <returns>所有图片</returns>
        IEnumerable<IPhoto> GetAllModels();

        /// <summary>
        /// 获取指定图片
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的图片</returns>
        IEnumerable<IPhoto> GetModels(Func<IPhoto, bool> exp);

        /// <summary>
        /// 获取符合条件的图片的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的图片的数量</returns>
        int GetCount(Func<IPhoto, bool> exp);

        /// <summary>
        /// 新建默认数据图片
        /// </summary>
        /// <returns>默认数据图片</returns>
        IPhoto CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析图片
        /// </summary>
        /// <param name="jsonString">包含图片信息的JSON字符串</param>
        /// <returns>解析后的图片</returns>
        IPhoto ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改图片
        /// </summary>
        /// <param name="model">被添加或修改的图片</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IPhoto model);

        /// <summary>
        /// 批量添加图片
        /// </summary>
        /// <param name="models">被添加或修改的图片列表</param>
        /// <returns>成功添加或修改的图片数量</returns>
        int AddOrUpdate(IEnumerable<IPhoto> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IPhoto model);

        /// <summary>
        /// 批量删除图片
        /// </summary>
        /// <param name="models">被删除的图片列表</param>
        /// <returns>成功删除的图片数量</returns>
        int Delete(IEnumerable<IPhoto> models);

        /// <summary>
        /// 判断图片是否存在
        /// </summary>
        /// <param name="model">被判断的图片</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IPhoto model);

        /// <summary>
        /// 判断图片是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IPhoto, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的图片</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IPhoto model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的图片数量</returns>
        void MarkDelete(IEnumerable<IPhoto> models);

        /// <summary>
        /// 设置图片启用状态
        /// </summary>
        /// <param name="model">设置启用状态的图片</param>
        /// <param name="enableStatus">Ture设置图片状态为启用，False设置图片状态为未启用</param>
        void SetEnableStatus(IPhoto model, bool enableStatus);

        /// <summary>
        /// 批量设置图片启用状态
        /// </summary>
        /// <param name="models">设置启用状态的图片列表</param>
        /// <param name="enableStatus">Ture设置图片状态为启用，False设置图片状态为未启用</param>
        void SetEnableStatus(IEnumerable<IPhoto> models, bool enableStatus);
    }
}