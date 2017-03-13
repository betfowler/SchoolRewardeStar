using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Product
    {
        [Key]
        public int Product_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int ProductCategory_ID { get; set; }

        public virtual ProductCategory ProductCategories { get; set; }
        public virtual Order Orders { get; set; }
    }

    public class ProductCategory
    {
        [Key]
        public int ProductCategory_ID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}