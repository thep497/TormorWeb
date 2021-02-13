using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using NNS.Config;

namespace NNS.Web.Infrastructure
{
    public class LocaleDateTimeNModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result == null)
            {
                return null;
            }

            var date = result.AttemptedValue;

            if (String.IsNullOrEmpty(date))
            {
                return null;
            }

            try
            {
                return DateTime.ParseExact(date, Globals.DateTimeFormat, Thread.CurrentThread.CurrentCulture);
            }
            catch
            {
                try
                {
                    return DateTime.ParseExact(date, Globals.DateFormat, Thread.CurrentThread.CurrentCulture);
                }
                catch
                {
                    try
                    {
                        return DateTime.Parse(date, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None);
                    }
                    catch
                    {
                        return DateTime.Today;
                    }
                }
            }
        }
    }
    public class LocaleDateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var date = result.AttemptedValue;

            if (String.IsNullOrEmpty(date))
            {
                return DateTime.MinValue;
            }

            try
            {
                return DateTime.ParseExact(date, Globals.DateTimeFormat,Thread.CurrentThread.CurrentCulture);
            }
            catch
            {
                try
                {
                    return DateTime.ParseExact(date, Globals.DateFormat, Thread.CurrentThread.CurrentCulture);
                }
                catch
                {
                    try
                    {
                        return DateTime.Parse(date, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None);
                    }
                    catch
                    {
                        return DateTime.Today;
                    }
                }
            }
        }
    }
}