using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Commerce.Models;
namespace E_Commerce.ViewModels
{
    public class CartView
    {
        public float TotalCost { get; set; }
        public IEnumerable<Cart> carts { get; set; }
        public User user { get; set; }
    }
}