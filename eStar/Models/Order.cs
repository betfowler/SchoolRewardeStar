using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Order ID")]
        public int Order_ID { get; set; }
        public int User_ID { get; set; }
        [Display(Name = "Order Status")]
        public int OrderStatus_ID { get; set; }
        [Display(Name ="Total Cost")]
        public int TotalCost { get; set; }

        public List<ProductOrder> Products { get; set; }
        [Display(Name = "No. Items")]
        public int ProductCount { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }

    public class OrderStatus
    {
        [Key]
        public int OrderStatus_ID { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}