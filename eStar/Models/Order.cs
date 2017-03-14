﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        public int User_ID { get; set; }
        public int Quantity { get; set; }
        public int OrderStatus_ID { get; set; }

        public List<int> Products { get; set; }
        public int ProductCount { get; set; }
        public List<string> ProductNames { get; set; }

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