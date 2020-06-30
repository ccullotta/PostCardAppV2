using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace PostCardAppV2.Models.dtos
{
    internal class DictionaryValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value.GetType() != typeof(Dictionary<String, String>))
            {
                return new ValidationResult("Invalid Format, valid input is of the form ##.## or ###");
            }
            Dictionary<String, string> dictionary = (Dictionary<String, string>)value;
            double output = 0;
            if (dictionary.Values.Any(x => !double.TryParse(x, out output) || output < 0))
            {
                return new ValidationResult("Invalid Format, valid input is of the form ##.## or ### and all valid inputs are above zero");
            }
            return ValidationResult.Success;
            
        }
    }
}