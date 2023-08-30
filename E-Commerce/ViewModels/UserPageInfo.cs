using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Commerce.Models;
namespace E_Commerce.ViewModels
{
    public class UserPageInfo
    {
        public User user { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}