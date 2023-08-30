using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        DbModel db = new DbModel();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

    }
}