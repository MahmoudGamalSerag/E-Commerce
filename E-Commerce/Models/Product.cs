using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Product
    {
        public int ID { get; set; }
        public float cost { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public bool Exist { get; set; }
        public int? updated { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
    }
}