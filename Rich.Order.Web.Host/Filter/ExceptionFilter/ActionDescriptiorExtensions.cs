﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Rich.Order.Web.Host.Filter.ExceptionFilter
{
    public static class ActionDescriptiorExtensions
    {
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            if (!actionDescriptor.IsControllerAction())
            {
                throw new Exception($"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
            }

            return actionDescriptor as ControllerActionDescriptor;
        }

        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }

        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
    }
}
