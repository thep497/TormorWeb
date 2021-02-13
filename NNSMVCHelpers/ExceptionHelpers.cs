using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NNS.ModelHelpers;

namespace NNS.MVCHelpers
{
    public static class RulesViolationExceptionExtensions
    {
        public static void CopyTo(this RulesException ex, ModelStateDictionary modelState)
        {
            CopyTo(ex, modelState, null);
        }

        public static void CopyTo(this RulesException ex, ModelStateDictionary modelState, string prefix)
        {
            prefix = string.IsNullOrEmpty(prefix) ? "" : prefix + ".";
            foreach (var propertyError in ex.Errors)
            {
                string key = ExpressionHelper.GetExpressionText(propertyError.Property);
                modelState.AddModelError(prefix + key, propertyError.Message);
            }
        }
    }
}