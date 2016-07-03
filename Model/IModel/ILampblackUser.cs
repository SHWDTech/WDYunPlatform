using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 油烟用户模型接口
    /// </summary>
    public interface ILampblackUser : IWdUser
    {
        /// <summary>
        /// 用户部门
        /// </summary>
        Department Department { get; set; }

        /// <summary>
        /// 用户部门ID
        /// </summary>
        Guid DepartmentId { get; set; }

        /// <summary>
        /// 用户机构
        /// </summary>
        CateringCompany CateringCompany { get; set; }

        /// <summary>
        /// 用户机构ID
        /// </summary>
        Guid CateringCompanyId { get; set; }
    }
}
