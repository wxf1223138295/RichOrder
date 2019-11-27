using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rich.Common.Base.RichReturnModel;
using Serilog;

namespace Rich.Order.Web.Host.Filter.ExceptionFilter
{
    public class RichExctption: IExceptionFilter
    {
        private ILogger _logger;

        public RichExctption(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var oper=context.HttpContext.User.Identity.Name;
            var obj=new object[]{ oper , context .HttpContext.TraceIdentifier,context.Exception.Message,DateTime.Now};
            _logger.Error("",obj);
            //参数校验异常
            if (!context.ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        builder.Append(error.ErrorMessage + "|");
                    }
                }
                context.Result = new ObjectResult(
                    new RichApiReturnBoolModel(50000, false, builder.ToString(),
                        context.Exception.Message));
            }
            else
            {

                context.Result = new ObjectResult(
                    new RichApiReturnBoolModel(50000, false, context.Exception.Message, context.Exception.InnerException?.Message));
            }
            //防止程序卡死    
            context.Exception = null;
        }
    }
}
