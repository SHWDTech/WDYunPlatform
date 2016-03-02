using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 数据模型基类
    /// </summary>
    [Serializable]
    public class DataModelBase : ModelBase, IDataModel
    {
        [Required]
        [Display(Name = "所属域")]
        public ISysDomain Domain { get; set; }
    }
}
