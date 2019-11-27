using System.ComponentModel;

namespace Rich.Common.Base.RichReturnModel
{
    /// <summary>
    /// 请求参数的分页信息
    /// </summary>
    public class RequestPageInfo : IRequestPageInfo
    {
        public RequestPageInfo():this(1, 20)
        {
        }

        public RequestPageInfo(int page, int pageSize)
        {
            this.page = page;
            this.pageSize = pageSize;
        }

        /// <inheritdoc />
        public int page { get; set; }

        /// <inheritdoc />
        public int pageSize { get; set; }
        
    }

}