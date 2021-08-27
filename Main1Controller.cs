using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL;
using onlineshopping.Models;

namespace onlineshopping.Controllers
{
    public class Main1Controller : Controller
    {
        ProductDal dal = new ProductDal();
        // GET: Main1
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ShowProducts()
        {
            List<M_Products> p1 = new List<M_Products>();
            List<Products> data = dal.GetProducts();
            M_Products c = new M_Products();
            foreach (var item in data)
            {
                
                c.Product_ID = item.Product_ID;
                c.Product_Name = item.Product_Name;
                c.Specification = item.Specification;
                c.Price = item.Price;
                c.Product_image = item.Product_image;
                p1.Add(c);

            }
            ViewBag.pname = c.Product_ID;
             return View(p1);
        }
    }
}