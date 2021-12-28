using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class Validation
    {
        public static string PropError(string columnName, object obj)
        {
            var validationResults = new List<ValidationResult>();
            var value = obj.GetType().GetProperty(columnName).GetValue(obj);
            var validationContext = new ValidationContext(obj) { MemberName = columnName };

            if (Validator.TryValidateProperty(value, validationContext, validationResults))
                return null;

            return validationResults.First().ErrorMessage;
        }
        public static string ErorrCheck(IDataErrorInfo obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(typeof(ValidationAttribute), false).Length == 0) continue;

                if (obj[prop.Name] != null)
                    return "One or more of the properties are not correct";

            }
            return null;
        }
    }
}
