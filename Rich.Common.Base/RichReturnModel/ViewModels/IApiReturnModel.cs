using System.ComponentModel;

namespace Rich.Common.Base.RichReturnModel
{

    public interface IApiReturnModel<TReturnData>:IRichReturnModel
    {
        /// <summary>
        /// 返回码
        /// </summary>
        int resultCode { get; set; }
        /// <summary>
        /// 成功失败标识
        /// </summary>
        bool success { get; set; }
        /// <summary>
        /// 接口返回数据，可以是基本类型、object、数组
        /// </summary>
        TReturnData data { set; get; }
        /// <summary>
        /// 字符串类型，操作结果说明，比如错误提示——使用于界面显示
        /// </summary>
        string message { set; get; }
        /// <summary>
        /// 分页数据。如无分页，可为空,空则ignore
        /// </summary>
        IResponsePageInfo pageInfo { set; get; }
        /// <summary>
        /// 异常信息。使用于内部调试。详细异常日志记录文本日志，抽入日志中心
        /// </summary>
        string exceptionMsg { set; get; }
        /// <summary>
        /// 请求跟踪ID
        /// </summary>
        /// <returns></returns>
        string logTraceId { set; get; }
        /// <summary>
        /// 格式和逻辑校验，要求序列化前必须校验
        /// </summary>
        /// <returns></returns>
        bool Validate();
        /// <summary>
        /// 标准序列化JSON方法
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
