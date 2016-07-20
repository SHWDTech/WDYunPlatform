namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 项目模型接口
    /// </summary>
    public interface IProject : ISysDomainModel
    {
        string ProjectCode { get; set; }

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
        float? Longitude { get; set; }

        /// <summary>
        /// 项目坐标纬度
        /// </summary>
        float? Latitude { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        string Comment { get; set; }
    }
}