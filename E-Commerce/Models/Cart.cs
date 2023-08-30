using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public User user { get; set; }
        public int userid { get; set; }
        public Product product { get; set; }
        public int productid { get; set; }
        public Order order { get; set; }
        public int? orderid { get; set; }
    }
}