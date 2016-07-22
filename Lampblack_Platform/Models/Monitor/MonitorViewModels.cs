using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcWebComponents.Model;
using PagedList;
using Platform.Process.Business;
using SHWDTech.Platform.Model.Model;

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
        /// 街道ID
        /// </summary>
        [Display(Name = "街道")]
        public Guid StreetGuid { get; set; }

        /// <summary>
        /// 酒店列表
        /// </summary>
        public IPagedList<HotelRestaurant> Hotels { get; set; }

        public override int PageIndex { get; set; } = 1;

        public override int PageSize { get; set; } = 15;
    }

    public class ActualViewModel : PagedListViewModelBase
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
        /// 街道ID
        /// </summary>
        [Display(Name = "街道")]
        public Guid StreetGuid { get; set; }

        /// <summary>
        /// 地址ID
        /// </summary>
        [Display(Name = "地址")]
        public Guid AddressGuid { get; set; }

        /// <summary>
        /// 酒店状态
        /// </summary>
        public IPagedList<HotelActualStatus> HotelsStatus { get; set; }
    }
}