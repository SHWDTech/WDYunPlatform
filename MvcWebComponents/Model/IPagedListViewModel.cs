namespace MvcWebComponents.Model
{
    public interface IPagedListViewModel
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 页数总数
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        string QueryName { get; set; }
    }
}
