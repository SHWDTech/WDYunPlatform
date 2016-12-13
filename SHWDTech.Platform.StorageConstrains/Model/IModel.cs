using SHWDTech.Platform.StorageConstrains.Model.Enums;

namespace SHWDTech.Platform.StorageConstrains.Model
{
    public interface IModel
    {
        /// <summary>
        /// 模型状态
        /// </summary>
        ModelState ModelState { get; set; }

        /// <summary>
        /// 模型是否为新创建对象
        /// </summary>
        bool IsNew { get; }
    }
}
