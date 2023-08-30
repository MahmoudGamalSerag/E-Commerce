using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Payment
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Your First Name!!")]
        [Display(Name = "Full Name :")]
        public string FullName { get; set; }
       
        [Required(ErrorMessage = "Enter Your Email !!")]
        [Display(Name = "Email :")]
        public string Email { get; set; }
        
        [Display(Name = "Profile Image :")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Enter Your Mobile !!")]
        [Display(Name = "Mobile :")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter Your Address !!")]
        [Display(Name = "Address :")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Your Cridet Card Number !!")]
        [Display(Name = "Cridet Card :")]
        public string CridetCard { get; set; }
        public User user { get; set; }
        public int userid { get; set; }
    }
}