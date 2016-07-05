using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcWebComponents.Model;

namespace Lampblack_Platform.Models.Monitor
{
    public class MapHotelViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 区域集合
        /// </summary>
        public List<SelectListItem> AreaListItems { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [Display(Name = "区域")]
        public Guid AreaGuid { get; set; }

        /// <summary>
        /// 酒店列表
        /// </summary>
        public Dictionary<Guid, string> Hotels { get; set; }

        public override int PageIndex { get; set; } = 1;

        public override int PageSize { get; set; } = 15;
    }
}