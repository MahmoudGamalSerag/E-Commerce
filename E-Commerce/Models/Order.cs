using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Order
    {
        public int ID { get; set; }
        public User user { get; set; }
        public int userid { get; set; }
        public float price { get; set; }
    }
}