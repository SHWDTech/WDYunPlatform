﻿namespace SHWDTech.Platform.Model.IModel
{
    public interface ISysDomain : ISysModel
    {
        /// <summary>
        /// 域名称
        /// </summary>
        string DomainName { get; set; }

        /// <summary>
        /// 域是否已经启用
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 域类型
        /// </summary>
        string DomianType { get; set; }
    }
}
