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
    public class UniqueSizeProductAttribute : ValidationAttribute
    {
        public string ProductIdProperty { get; }
        public string SizeIdProperty { get; }

        public UniqueSizeProductAttribute(string productIdProperty, string sizeIdProperty)
        {
            ProductIdProperty = productIdProperty;
            SizeIdProperty = sizeIdProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var productIdValue = validationContext.ObjectType.GetProperty(ProductIdProperty)?.GetValue(validationContext.ObjectInstance);
            var sizeValue = validationContext.ObjectType.GetProperty(SizeIdProperty)?.GetValue(validationContext.ObjectInstance);

            if (productIdValue is int productId && sizeValue is int sizeId) {
                var dbContext = (NamasStudioContext)validationContext.GetService(typeof(NamasStudioContext));
                var productStock = dbContext.StockProducts
                                   .FirstOrDefault(x => x.ProductId == Convert.ToInt32(productIdValue) && x.SizeId == Convert.ToInt32(sizeValue));
                if(productStock != null)
                {
                    return new ValidationResult(ErrorMessage ?? "Product with the same size already exists.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
