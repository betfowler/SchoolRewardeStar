using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    [Validator(typeof(ProductValidator))]
    public class Product
    {
        [Key]
        public int Product_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Product Category")]
        public int ProductCategory_ID { get; set; }
        public int Stock { get; set; }

        public virtual ProductCategory ProductCategories { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }

    public class ProductCategory
    {
        [Key]
        [Display(Name = "Product Category")]
        public int ProductCategory_ID { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(Product => Product.Name).NotNull().Length(0, 22).WithMessage("Product name entered is too long, it must be less than 22 characters.");
            RuleFor(Product => Product.Description).Length(0, 80).WithMessage("Product description must be less than 80 characters.");
        }
    }
}