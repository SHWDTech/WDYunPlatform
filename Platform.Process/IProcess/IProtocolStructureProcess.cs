﻿using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议结构处理接口
    /// </summary>
    public interface IProtocolStructureProcess
    {
        /// <summary>
        /// 获取所有协议结构
        /// </summary>
        /// <returns>所有协议结构</returns>
        IEnumerable<IProtocolStructure> GetAllModels();

        /// <summary>
        /// 获取指定协议结构
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的协议结构</returns>
        IEnumerable<IProtocolStructure> GetModels(Func<IProtocolStructure, bool> exp);

        /// <summary>
        /// 获取符合条件的协议结构的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的协议结构的数量</returns>
        int GetCount(Func<IProtocolStructure, bool> exp);

        /// <summary>
        /// 新建默认数据协议结构
        /// </summary>
        /// <returns>默认数据协议结构</returns>
        IProtocolStructure CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析协议结构
        /// </summary>
        /// <param name="jsonString">包含协议结构信息的JSON字符串</param>
        /// <returns>解析后的协议结构</returns>
        IProtocolStructure ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改协议结构
        /// </summary>
        /// <param name="model">被添加或修改的协议结构</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IProtocolStructure model);

        /// <summary>
        /// 批量添加协议结构
        /// </summary>
        /// <param name="models">被添加或修改的协议结构列表</param>
        /// <returns>成功添加或修改的协议结构数量</returns>
        int AddOrUpdate(IEnumerable<IProtocolStructure> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IProtocolStructure model);

        /// <summary>
        /// 批量删除协议结构
        /// </summary>
        /// <param name="models">被删除的协议结构列表</param>
        /// <returns>成功删除的协议结构数量</returns>
        int Delete(IEnumerable<IProtocolStructure> models);

        /// <summary>
        /// 判断协议结构是否存在
        /// </summary>
        /// <param name="model">被判断的协议结构</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IProtocolStructure model);

        /// <summary>
        /// 判断协议结构是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IProtocolStructure, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的协议结构</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IProtocolStructure model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的协议结构数量</returns>
        void MarkDelete(IEnumerable<IProtocolStructure> models);

        /// <summary>
        /// 设置协议结构启用状态
        /// </summary>
        /// <param name="model">设置启用状态的协议结构</param>
        /// <param name="enableStatus">Ture设置协议结构状态为启用，False设置协议结构状态为未启用</param>
        void SetEnableStatus(IProtocolStructure model, bool enableStatus);

        /// <summary>
        /// 批量设置协议结构启用状态
        /// </summary>
        /// <param name="models">设置启用状态的协议结构列表</param>
        /// <param name="enableStatus">Ture设置协议结构状态为启用，False设置协议结构状态为未启用</param>
        void SetEnableStatus(IEnumerable<IProtocolStructure> models, bool enableStatus);
    }
}