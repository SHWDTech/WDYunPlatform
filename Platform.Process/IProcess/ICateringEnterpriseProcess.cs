using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 餐饮企业、饭店处理程序接口
    /// </summary>
    public interface ICateringEnterpriseProcess
    {
        /// <summary>
        /// 获取餐饮企业列表
        /// </summary>
        /// <returns></returns>
        List<CateringCompany> GetCateringCompanies();

        /// <summary>
        /// 添加或更新餐饮企业
        /// </summary>
        /// <param name="model"></param>
        void AddOrUpdateCateringEnterprise(CateringCompany model);
    }
}
