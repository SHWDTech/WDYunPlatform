using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 用户自定义字典模型接口
    /// </summary>
    public interface IUserDictionary : ISysDomainModel
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        string ItemName { get; set; }

        /// <summary>
        /// 字典项关键字
        /// </summary>
        string ItemKey { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        string ItemValue { get; set; }

        /// <summary>
        /// 字典项层级
        /// </summary>
        int ItemLevel { get; set; }

        /// <summary>
        /// 父级字典项
        /// </summary>
        SysDictionary ParentDictionary { get; set; }
    }
}