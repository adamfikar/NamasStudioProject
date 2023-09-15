using NamasStudio.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Validations
{

    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var categoryName = value as string;
            if (string.IsNullOrEmpty(categoryName))
            {
                // CategoryName is required, so return validation error if it's empty or null.
                return new ValidationResult("Category name cannot be empty.");
            }

            using var dbContext = new NamasStudioContext();
            bool isNameExist = dbContext.CategoryProducts.Any(x => x.CategoryName == (string)value);

            if (isNameExist)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
