namespace Rich.Common.Base.RichReturnModel
{
    /// <summary>
    /// 返回参数的分页信息
    /// </summary>
    public interface IResponsePageInfo
    {
        /// <summary>
        /// 页码，默认从1开始
        /// </summary>
        int page { set; get; }
        /// <summary>
        /// 每页大小，要求必须在5-200之间，默认20
        /// </summary>
        int pageSize { set; get; }
        /// <summary>
        /// 条目总数量
        /// </summary>
        int totalCount { set; get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int pageCount { get; }
    }

}