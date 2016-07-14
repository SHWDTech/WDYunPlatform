namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 设备型号模型接口
    /// </summary>
    public interface IDeviceModel : ISysDomainModel
    {
        /// <summary>
        /// 设备型号名称
        /// </summary>
        string Name { get; set; }
    }
}
