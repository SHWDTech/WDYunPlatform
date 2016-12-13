using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.StorageConstrains.Model
{
    /// <summary>
    /// GUID主键
    /// </summary>
    public interface IGuidKey
    {
        /// <summary>
        /// GUID主键。
        /// </summary>
        [Key]
        Guid Id { get; set; }
    }
}
