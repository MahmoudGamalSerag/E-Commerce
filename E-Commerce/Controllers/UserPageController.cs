using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using E_Commerce.ViewModels;
namespace E_Commerce.Controllers
{
    public class UserPageController : Controller
    {
        DbModel db = new DbModel();
        // GET: UserPage
        public ActionResult Index(User user)
        {
            var products = db.Products.ToList();
            UserPageInfo pageinfo = new UserPageInfo { user = user, Products = products };
            return View(pageinfo);
        }



        ///////////////////////////////////////////////////
        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(c => c.ID == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", user);
            }

            if (user.imageFile != null)
            {

                string fileName = Path.GetFileNameWithoutExtension(user.imageFile.FileName);
                string extensions = Path.GetExtension(user.imageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extensions;
                user.Image = "/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
                user.imageFile.SaveAs(fileName);
            }

            var userDB = db.Users.Single(c => c.ID == user.ID);
            userDB.ID = user.ID;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            if (user.Image != null)
            {
                userDB.Image = user.Image;
            }
            userDB.Mobile = user.Mobile;
            userDB.Email = user.Email;
            userDB.Gender = user.Gender;
            userDB.Password = user.Password;
            userDB.RePassword = user.Password;
            db.SaveChanges();
            return RedirectToAction("Search", userDB);

        }
        public ActionResult cartfun(int id, int id2)
        {
            User user = db.Users.Single(x => x.ID == id2);
            try
            {
                Cart car = new Cart { userid = id2,productid=id,orderid=null};
                db.carts.Add(car);
                db.SaveChanges();
            }
            catch
            {
               return RedirectToAction("Search", user);
            }
            return RedirectToAction("Search", user);
        }
        ///////////////////////////////////////////////////////////
        public ActionResult cart(int id)
        {
            var mycart = db.carts.Where(x => x.userid == id&&x.orderid==null).ToList();
            float totalcost = 0;
            foreach (var i in mycart)
            {
                i.user = db.Users.Single(x => x.ID == i.userid);
                i.product = db.Products.Single(x => x.ID == i.productid&&x.Exist);
                totalcost += i.product.cost;
            }
            CartView cart = new CartView
            {
                TotalCost = totalcost,
                carts = mycart,
                user = db.Users.Single(x => x.ID == id)
            };
            return View(cart);
        }

        /////////////////////////////////////////////////////////////////
        ///
        public ActionResult Search(User user)
        {
            var productlist = db.Products.Where(x=>x.Exist==true).ToList();
           
            SearchModelView searchModelview = new SearchModelView
            {
                user = user,
                products = productlist
                //SerachUsers = db.Users.Where(x=>x.ID!=id).ToList()
            };
            return View(searchModelview);
        }
        [HttpGet]
        public async Task<ActionResult> Search(string productsearch, int id)
        {
            User user = db.Users.Single(x => x.ID == id);
            ViewData["productsearch"] = productsearch;
            var emptquery = from x in db.Products.Where(i=>i.Exist==true) select x;

            if (!string.IsNullOrEmpty(productsearch))
            {
                emptquery = emptquery.Where(x => x.title.Contains(productsearch));
            }
            SearchModelView searchModelview = new SearchModelView
            {
                user = user,
                products = await emptquery.AsNoTracking().ToListAsync()
            };
           
            return View(searchModelview);
        }
        /////////////////////////////////////////////////////////////////
        ///
        public ActionResult RemoveProduct(int id, int id2)
        {
            User user = db.Users.Single(x => x.ID == id);
            var carts = db.carts.Where(x => x.userid == id && x.productid == id2).ToList();
            Cart cart = carts[0];
            try
            {

              db.carts.Remove(cart);
              db.SaveChanges();
                return RedirectToAction("Cart",new { id = id });
            }
            catch
            {

            return RedirectToAction("Cart",new { id = id });
            }
        }
        ///////////////////////////////////////
        ///[HttpGet]
        public ActionResult PaymentFun(int id)
        {
            User user = db.Users.Single(c => c.ID == id);
            Payment payment = new Payment
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Address = user.Address,
                Mobile = user.Mobile,
                Image = user.Image,
                userid=id,
                user=db.Users.Single(c => c.ID == id)
            };
            return View(payment);
        }
        [HttpPost]
        public ActionResult PaymentFun(Payment  payment)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentFun", payment);
            }
            db.payments.Add(payment);
            db.SaveChanges();
            int id = payment.userid;
            return RedirectToAction("MakeOrder", new { id = id });

        }
        ///////////////////////////////////////////////////////////////////////////////
        public ActionResult MakeOrder(int id)
        {
            
            var mycart = db.carts.Where(x => x.userid == id && x.orderid == null).ToList();
            float totalcost = 0;
            foreach (var i in mycart)
            {
                i.user = db.Users.Single(x => x.ID == i.userid);
                i.product = db.Products.Single(x => x.ID == i.productid);
                totalcost += i.product.cost;
            }
            
            CartView cart = new CartView
            {
                TotalCost = totalcost,
                carts = mycart,
                user = db.Users.Single(x => x.ID == id)
            };
            if (totalcost == 0)
            {
                return View(cart);
            }
            Order order = new Order
            {
                user = cart.user,
                price = totalcost
            };
            try
            {
                db.orders.Add(order);
                db.SaveChanges();
                foreach (var i in mycart)
                {
                    i.orderid = order.ID;
                    db.SaveChanges();
                }

            }
            catch
            {
               return View(cart);
            }
            return RedirectToAction("Cart", new { id = id });
        }

    }
}