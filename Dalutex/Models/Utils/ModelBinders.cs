using System;
using System.Globalization;
using System.Web.Mvc;

namespace Dalutex.Models.Utils
{
    public class DecimalModelBinders : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                string sAttemptedValue = valueResult.AttemptedValue.Replace(".", ",");

                actualValue = Convert.ToDecimal(sAttemptedValue, CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }

    //public class IntegerModelBinders : IModelBinder
    //{
    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    //        ModelState modelState = new ModelState { Value = valueResult };
    //        object actualValue = null;
    //        try
    //        {
    //            string sAttemptedValue = valueResult.AttemptedValue.Replace(".", ",");

    //            actualValue = Convert.ToInt32(sAttemptedValue, CultureInfo.CurrentCulture);
    //        }
    //        catch (FormatException e)
    //        {
    //            modelState.Errors.Add(new FormatException("Digite apenas valores inteiros."));
    //        }

    //        bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
    //        return actualValue;
    //    }
    //}
}