using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Rich.Common.Base.RichReturnModel
{
    /// <summary>
    /// 瑞慈标准接口返回参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject(MemberSerialization.OptOut)]
    public class RichApiReturnModel<T> : IApiReturnModel<T>
    {
        public RichApiReturnModel(int resultCode, bool success, T data, int page, int pageSize, int totalCount, string message = null, string exceptionMsg = null, string logTraceId = null)
        {
            this.resultCode = resultCode;
            this.success = success;
            this.data = data;
            this.message = message;
            pageInfo = new ResponsePageInfo(page, pageSize, totalCount);
            this.exceptionMsg = exceptionMsg;
            this.logTraceId = logTraceId;

            if (!this.success)
            {
                if (resultCode == Const.GeneralSuccessCode)
                {
                    throw new ArgumentException("返回码不正确", nameof(resultCode));
                }
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException("请指定错误信息", nameof(message));
                }
            }
        }
        public RichApiReturnModel(int resultCode, bool success, T data, string message = null, string exceptionMsg = null, string logTraceId = null)
        {
            this.resultCode = resultCode;
            this.success = success;
            this.data = data;
            this.message = message;
            this.exceptionMsg = exceptionMsg;
            this.logTraceId = logTraceId;
            if (!this.success)
            {
                if (resultCode == Const.GeneralSuccessCode)
                {
                    throw new ArgumentException("返回码不正确", nameof(resultCode));
                }
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException("请指定错误信息", nameof(message));
                }
            }
        }

        public RichApiReturnModel(T data, string message = null, string logTraceId = null): this(Const.GeneralSuccessCode, true, data, message, null, logTraceId)
        {

        }

        public RichApiReturnModel(T data, int page, int pageSize, int totalCount, string message = null, string logTraceId = null): this(Const.GeneralSuccessCode, true, data, page, pageSize, totalCount, message, null, logTraceId)
        {

        }
        public RichApiReturnModel(int resultCode, bool success, string message = null, string exceptionMsg = null, string logTraceId = null)
            : this(resultCode, success, default(T), message, exceptionMsg, logTraceId)
        {
        }
        public RichApiReturnModel(string message, string exceptionMsg = null, string logTraceId = null)
            : this(Const.GeneralExceptionErrorCode, false, default(T), message, exceptionMsg, logTraceId)
        {
        }

        public RichApiReturnModel(int resultCode, Exception ex, string message, string logTraceId = null) : this(resultCode, false, default(T), message, null, logTraceId)
        {
            success = false;
            if (ex != null)
            {
                exceptionMsg = "发生异常：" + ex.Message;
            }
        }
        public RichApiReturnModel(Exception ex, string message, string logTraceId = null) : this(Const.GeneralExceptionErrorCode, ex, message, logTraceId)
        {

        }
        public RichApiReturnModel(Exception ex, string logTraceId = null) : this(Const.GeneralExceptionErrorCode, ex, null, logTraceId)
        {

        }
        public RichApiReturnModel()
        {
            success = true;
            resultCode = Const.GeneralSuccessCode;
        }

        /// <summary>
        /// 返回码
        /// </summary>
        [DefaultValue(20000)]
        public int resultCode { get; set; }

        /// <summary>
        /// 成功失败标识
        /// </summary>
        [DefaultValue(true)]
        public bool success { get; set; }

        /// <summary>
        /// 接口返回数据，可以是基本类型、object、数组
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 字符串类型，操作结果说明，比如错误提示——使用于界面显示
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 分页数据。如无分页，可为空,空则ignore
        /// </summary>
        public IResponsePageInfo pageInfo { get; set; }

        /// <summary>
        /// 异常信息。使用于内部调试。详细异常日志记录文本日志，抽入日志中心
        /// </summary>
        public string exceptionMsg { get; set; }

        /// <summary>
        /// 请求跟踪ID
        /// </summary>r
        /// <returns></returns>
        public string logTraceId { get; set; }
        [JsonIgnore]
        public string validateErrors { get; protected set; }



        /// <inheritdoc />
        /// <summary>
        /// 格式和逻辑校验，要求序列化前必须校验
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            validateErrors = string.Empty;
            //            if (string.IsNullOrEmpty(logTraceId))
            //            {
            //                logTraceId = 
            //            }
            if (success && exceptionMsg != null)
            {
                validateErrors += "返回参数校验失败：接口返回成功，但错误日志不为空；";
            }

            if (pageInfo != null)
            {
                if (pageInfo.page <= 0)
                {
                    validateErrors += "返回页码应大于0；";
                }

                if (pageInfo.pageSize > 200 || pageInfo.pageSize <= 0)
                {
                    validateErrors += "返回页大小应该在5-200之间；";
                }
            }
            return string.IsNullOrEmpty(validateErrors);
        }

        /// <inheritdoc />
        /// <summary>
        /// 标准序列化JSON方法
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            if (!Validate())
            {
                exceptionMsg += validateErrors;
                throw new ArgumentException("返回参数格式校验失败，请注意检查：" + validateErrors, GetType().Name);
            }
            JsonSerializerSettings jsetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = Const.DateTimeFormat
            };            
            var strJson=JsonConvert.SerializeObject(this, jsetting);
            return strJson;
        }
    }
}
