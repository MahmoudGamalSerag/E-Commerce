using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Commerce.Models;
namespace E_Commerce.ViewModels
{
    public class SearchModelView
    {
        public User user { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}