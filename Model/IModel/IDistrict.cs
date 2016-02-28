using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    internal interface IDistrict
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        int DistrictId { get; set; }

        /// <summary>
        /// 父级区域
        /// </summary>
        District ParentDistrict { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        string DistrictName { get; set; }

        /// <summary>
        /// 区域类型
        /// </summary>
        string DistrictType { get; set; }
    }
}
