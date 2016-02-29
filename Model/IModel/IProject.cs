using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IProject
    {
        /// <summary>
        /// 项目所属域
        /// </summary>
        SysDomain ProjectDomain { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        string ProjectCode { get; set; }

        /// <summary>
        /// 项目外部编码
        /// </summary>
        string ProjectOutCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        string ChargeMan { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        string Telephone { get; set; }

        /// <summary>
        /// 项目坐标经度
        /// </summary>
        float Longitude { get; set; }

        /// <summary>
        /// 项目坐标纬度
        /// </summary>
        float Latitude { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        string Compnany { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// 项目所在街道
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// 项目所属区域
        /// </summary>
        District District { get; set; }

        /// <summary>
        /// 项目占地面积
        /// </summary>
        short Square { get; set; }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// 项目所属阶段
        /// </summary>
        int Stage { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        int Type { get; set; }

        /// <summary>
        /// 项目报警类型
        /// </summary>
        int AlarmType { get; set; }

        /// <summary>
        /// 项目是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
