using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IParticulateMatterProject : IProject
    {
        /// <summary>
        /// 监测点编号
        /// </summary>
        int StatCode { get; set; }

        /// <summary>
        /// 工程报建号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        string StatBJH { get; set; }

        /// <summary>
        /// 项目外部编码
        /// </summary>
        string ProjectOutCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        string StatName { get; set; }

        /// <summary>
        /// 施工单位
        /// </summary>
        string Department { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        string Compnany { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// 所属区县
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// 项目所在街道
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// 项目所属区域ID
        /// </summary>
        Guid DistrictId { get; set; }

        /// <summary>
        /// 项目所属区域
        /// </summary>
        SysDictionary District { get; set; }

        /// <summary>
        /// 项目占地面积
        /// </summary>
        short Square { get; set; }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        DateTime ProStartDate { get; set; }

        /// <summary>
        /// 施工进展情况
        /// </summary>
        string Stage { get; set; }

        /// <summary>
        /// 项目所属阶段ID
        /// </summary>
        Guid? ProjectStageId { get; set; }

        /// <summary>
        /// 项目所属阶段
        /// </summary>
        SysDictionary ProjectStage { get; set; }

        /// <summary>
        /// 项目类型ID
        /// </summary>
        Guid? TypeId { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        SysDictionary Type { get; set; }

        /// <summary>
        /// 项目报警类型ID
        /// </summary>
        Guid? AlarmTypeId { get; set; }

        /// <summary>
        /// 项目报警类型
        /// </summary>
        SysDictionary AlarmType { get; set; }
    }
}
