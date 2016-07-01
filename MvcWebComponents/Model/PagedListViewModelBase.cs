namespace MvcWebComponents.Model
{
    /// <summary>
    /// 分页视图模型基础模型
    /// </summary>
    public class PagedListViewModelBase : ViewModelBase, IPagedListViewModel
    {
        public virtual int Count { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// 页数总数
        /// </summary>
        public virtual int PageCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public virtual int PageIndex { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public virtual string QueryName { get; set; }
    }
}
