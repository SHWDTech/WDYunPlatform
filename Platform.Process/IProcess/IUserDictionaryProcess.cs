﻿using SqlComponents.SqlExcute;
using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 用户词典处理程序接口
    /// </summary>
    public interface IUserDictionaryProcess : IProcessBase
    {
        /// <summary>
        /// 添加区域信息
        /// </summary>
        /// <param name="areaName">区域名称</param>
        /// <param name="areaLevel">区域层级</param>
        /// <param name="parentNode">父级区域</param>
        /// <returns></returns>
        object AddArea(string areaName, int areaLevel, Guid parentNode);

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <returns></returns>
        object GetAreaInfo();

        /// <summary>
        /// 修改区域信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        object EditArea(Guid itemId, string itemValue);

        /// <summary>
        /// 删除区域信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        SqlExcuteResult DeleteArea(Guid itemId);

        /// <summary>
        /// 获取某区域的子节点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<Guid, string> GetChildDistrict(Guid id);

        /// <summary>
        /// 获取指定ID的区域信息
        /// </summary>
        /// <param name="districtGuid"></param>
        /// <returns></returns>
        UserDictionary GetDistrict(Guid districtGuid);

        /// <summary>
        /// 根据ITEMNAME获取用户字典列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<UserDictionary> GetDictionaries(string name, int level);
    }
}
