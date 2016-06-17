namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 餐饮企业模型接口
    /// </summary>
    public interface IRestaurant : ISysDomainModel
    {
        /// <summary>
        /// 餐饮企业名称
        /// </summary>
        string RestaurantName { get; set; }

        /// <summary>
        /// 餐饮企业编码
        /// </summary>
        string RestaurantCode { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        string ChargeMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        string Telephone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        string Address { get; set; }
    }
}
