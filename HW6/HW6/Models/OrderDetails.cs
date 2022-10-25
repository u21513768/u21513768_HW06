using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//<!--Quintin d' Hotman de Villiers u21513768-->
namespace HW6.Models
{
    public class OrderDetails
    {
        public order Order { get; set; }
        public order_items OrderItem { get; set; }
        public product Product { get; set; }
    }
}