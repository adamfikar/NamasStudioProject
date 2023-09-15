using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckTypeFileAttribute : ValidationAttribute
    {
        public string _pathNameProperty { get; }

        public CheckTypeFileAttribute(string pathName)
        {
            _pathNameProperty = pathName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _pathNameValue = validationContext.ObjectType.GetProperty(_pathNameProperty)?.GetValue(validationContext.ObjectInstance);

            if (_pathNameValue is string pathName )
            {
                var fileExtension = Path.GetExtension(pathName).ToLower();

                if (fileExtension != ".jpg")
                {
                    return new ValidationResult($"Invalid");
                }
            }
            return ValidationResult.Success;
        }
    }
}
