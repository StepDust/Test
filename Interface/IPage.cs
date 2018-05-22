namespace Interface {
    /// <summary>
    /// 分页数据接口
    /// </summary>
    public interface IPage {

        /// <summary>
        /// 数据总行数
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 页面跳转链接，带参数
        /// 用于分页跳转
        /// </summary>
        string PageUrl { get; set; }

        /// <summary>
        /// 分页的HTML字符串
        /// </summary>
        string PageContent { get; set; }

        /// <summary>
        /// 执行分页
        /// </summary>
        void GetPageList();
    }
}
