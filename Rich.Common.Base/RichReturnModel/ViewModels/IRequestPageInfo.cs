using System.ComponentModel;

namespace Rich.Common.Base.RichReturnModel
{
    /// <summary>
    /// 请求参数的分页信息
    /// </summary>
    public interface IRequestPageInfo
    {
        /// <summary>
        /// 页码
        /// </summary>
        [DefaultValue(1)]
        int page { set; get; }
        /// <summary>
        /// 每页大小。要求最大不超过200,应在5-200之间
        /// </summary>
        [DefaultValue(20)]
        int pageSize { set; get; }
    }

}