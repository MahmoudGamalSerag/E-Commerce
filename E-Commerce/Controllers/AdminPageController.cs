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
    public class AdminPageController : Controller
    {
        DbModel db = new DbModel();
        // GET: AdminPage
        public ActionResult Index()
        {
            var orders = db.orders.Where(x => x.ID != null).ToList();
            try
            {

            foreach(var i in orders)
            {
                i.user = db.Users.Single(x => x.ID == i.userid);
            }
              return View(orders);
            }
            catch
            {
                return View(orders);
            }
        }


        /////////////////////////////////////////////////////////
        
        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            
            string fileName = Path.GetFileNameWithoutExtension(product.imageFile.FileName);
            string extensions = Path.GetExtension(product.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extensions;
            product.image = "/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
            product.imageFile.SaveAs(fileName);
            product.Exist = true;
            product.updated = null;
            using (DbModel db = new DbModel())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        /////////////////////////////////////////////////////////

        public ActionResult Order(int id)
        {
            var mycart = db.carts.Where(x =>x.orderid == id).ToList();
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
                user =mycart[0].user
            };
            return View(cart);
        }


        /////////////////////////////////////////////////////////


        public ActionResult AdminProducts()
        {
            var products = db.Products.Where(x => x.Exist == true).ToList();
            return View(products);
        }


        ///////////////////////////////////////////////////////
        public ActionResult Remove(int id)
        {
  
            Product product = db.Products.Single(x => x.ID==id );
            try
            {

                product.Exist = false;
                db.SaveChanges();
                return RedirectToAction("AdminProducts");
            }
            catch
            {

                return RedirectToAction("AdminProducts");
            }
        }
        ///////////////////////////////////////////////////////////// 

        public ActionResult UpdateFun(int id)
        {
            Product product = db.Products.Single(x => x.ID == id);
            Product product2 = new Product
            {
                title = product.title,
                cost = product.cost,
                image = product.image,
                Exist = true,
                updated = product.ID
            };
            db.Products.Add(product2);
            db.SaveChanges();
            return RedirectToAction("Update", new { id = product2.ID });

        }
        [HttpGet]
        public ActionResult Update(int id)
        {
             Product product = db.Products.Single(x => x.ID == id);
            
            return View(product);
          
        }
        [HttpPost]
        public ActionResult Update(Product product)
        {

            if (!ModelState.IsValid)
            {
                return View("Update", product);
            }
            try
            {
                var product2 = db.Products.Single(x => x.ID == product.updated);
                product2.updated = product.ID;
                product2.Exist = false;
                db.SaveChanges();
                var productDB = db.Products.Single(x => x.ID == product.ID);
                if (product.imageFile != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(product.imageFile.FileName);
                    string extensions = Path.GetExtension(product.imageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extensions;
                    product.image = "/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
                    product.imageFile.SaveAs(fileName);
                    productDB.image = product.image;
                }
                product.updated = null;
                productDB.ID = product.ID;
                productDB.title = product.title;
                productDB.updated = product.updated;
                
                productDB.Exist = product.Exist;
                productDB.cost = product.cost;
                db.SaveChanges();
                return RedirectToAction("UpdateCart",new { id = product2.ID});

            }
            catch
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("AdminProducts");
            }
        }
        public ActionResult UpdateCart(int id)
        {
            Product product = db.Products.Single(x => x.ID == id);
            var cart = db.carts.ToList();
            foreach(var i in cart)
            {
                if(i.productid==id&&i.orderid==null)
                {
                    if(product.updated!=null)
                    {
                        i.productid = (int)product.updated;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("AdminProducts");

        }

    }
}