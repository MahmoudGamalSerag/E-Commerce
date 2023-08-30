using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace E_Commerce.Controllers
{
    public class LoginController : Controller
    {
        DbModel db = new DbModel();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Account user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }
            try
            {
                if(user.Email=="Admin"&&user.Password=="12345")
                {
                    return RedirectToAction("Index","AdminPage");
                }

                User acc = db.Users.Single(x => x.Email == user.Email && x.Password == user.Password);
                int id = acc.ID;
                return RedirectToAction("Search", "UserPage",acc);
                //return RedirectToAction("Index", "UserPage", acc);
            }
            catch (Exception e)
            {
                ViewData["Message"] = "Wrong password or Email";

                return View("Index");
            }
        }


    }
}