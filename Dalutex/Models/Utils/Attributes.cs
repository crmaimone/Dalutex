using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalutex.Models.Utils
{
    public class GreaterThanTodayAttribute : ValidationAttribute
    {
        private bool _allowNull = false;

        public GreaterThanTodayAttribute(bool AllowNull)
        {
            this._allowNull = AllowNull;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                if (_allowNull)
                    return true;
                else
                    return false;
            }
            else
            {
                if (value is DateTimeOffset)
                {
                    DateTimeOffset dt = (DateTimeOffset)value;

                    if (dt.Date >= DateTime.Today)
                    {
                        return true;
                    }
                }
                else
                {
                    DateTime dt = (DateTime)value;

                    if (dt >= DateTime.Today)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }

    /// <summary>
    /// http://www.codeproject.com/Articles/613330/Building-Client-JavaScript-Custom-Validation-in-AS
    /// </summary>
    public class RequiredIfTrueAttribute : ValidationAttribute, IClientValidatable
    {
        public string BooleanPropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (GetValue<bool>(validationContext.ObjectInstance, BooleanPropertyName))
            {
                return new RequiredAttribute().IsValid(value) ?
                    ValidationResult.Success :
                    new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requirediftrue",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("boolprop", BooleanPropertyName);
            yield return modelClientValidationRule;
        }
    }

    public class UntilTodayAttribute : ValidationAttribute
    {
        private bool _allowNull = false;

        public UntilTodayAttribute(bool AllowNull)
        {
            this._allowNull = AllowNull;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                if (_allowNull)
                    return true;
                else
                    return false;
            }
            else
            {
                if (value is DateTimeOffset)
                {
                    DateTimeOffset dt = (DateTimeOffset)value;

                    if (dt.Date <= DateTime.Today)
                    {
                        return true;
                    }
                }
                else
                {
                    DateTime dt = (DateTime)value;

                    if (dt <= DateTime.Today)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }

    public class StringRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}