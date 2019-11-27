using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Rich.Common.Base.RichReturnModel
{
    /// <summary>
    /// 瑞慈标准接口返回参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject(MemberSerialization.OptOut)]
    public class RichApiReturnBoolModel : RichApiReturnModel<object>
    {
        public RichApiReturnBoolModel(bool success, string message=null) :base(Const.GeneralSuccessCode, success, message, null, null)
        {

        }
        public RichApiReturnBoolModel(int resultCode, bool success, string message=null, string exceptionMsg=null, string tracelogId = null):base(resultCode, success, message, exceptionMsg, tracelogId)
        {
        }
    }
}
