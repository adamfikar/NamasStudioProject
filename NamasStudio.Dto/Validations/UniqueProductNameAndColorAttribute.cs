using Microsoft.EntityFrameworkCore;
using NamasStudio.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueProductNameAndColorAttribute : ValidationAttribute
    {
        public string ProductNameProperty { get; }
        public string ColorProperty { get; }

        public UniqueProductNameAndColorAttribute(string productNameProperty, string colorProperty)
        {
            ProductNameProperty = productNameProperty;
            ColorProperty = colorProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var productNameValue = validationContext.ObjectType.GetProperty(ProductNameProperty)?.GetValue(validationContext.ObjectInstance);
            var colorValue = validationContext.ObjectType.GetProperty(ColorProperty)?.GetValue(validationContext.ObjectInstance);

            if (productNameValue is string productName && colorValue is string color)
            {
                // You should replace YourDbContext with your actual DbContext class.
                var dbContext = (NamasStudioContext)validationContext.GetService(typeof(NamasStudioContext));

                // Get the original entity from the database for comparison
                var productIdProperty = validationContext.ObjectType.GetProperty("ProductId");
                var productIdValue = productIdProperty?.GetValue(validationContext.ObjectInstance);
                int.TryParse(productIdValue?.ToString(), out int productId);

                if (productId != 0)
                {
                    var originalEntity = dbContext.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == productId);
                    if (originalEntity != null && originalEntity.Color != color)
                    {
                        // If the Color is changed and there's another product with the same name and color, return validation error.
                        if (dbContext.Products.Any(p => p.ProductName == productName && p.Color == color))
                        {
                            return new ValidationResult(ErrorMessage ?? "Product with the same name and color already exists.");
                        }
                    }
                }
                else
                {
                    // For new entities, check if there's any other product with the same name and color.
                    if (dbContext.Products.Any(p => p.ProductName == productName && p.Color == color))
                    {
                        return new ValidationResult(ErrorMessage ?? "Product with the same name and color already exists.");
                    }
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid data type for ProductName or Color.");
        }
    }
}
