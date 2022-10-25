using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW6.Models
{
    public class OrderDetails
    {
        public order Order { get; set; }
        public order_items OrderItem { get; set; }
        public product Product { get; set; }
    }
}