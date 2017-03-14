using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class ProductOrder
    {
        [Key]
        public int ProductOrder_ID { get; set; }
        public int Product_ID { get; set; }
        public int Order_ID { get; set; }

        public virtual Product Products { get; set; }
        public virtual Order Orders { get; set; }
    }
}