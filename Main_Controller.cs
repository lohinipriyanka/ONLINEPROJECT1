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
    public class Main_Controller : Controller
    {
        CategoriesDAL dal = new CategoriesDAL();
        ProductDal p = new ProductDal();
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Categories

        [HttpPost]
        public ActionResult Index(int id)
        {
            List<Categories> categories = dal.GetCategories();
            List<M_Category> mlist = new List<M_Category>();
            foreach (var item in categories)
            {
                M_Category c = new M_Category();
                c.CatID = item.Categori_ID;
                c.CatName = item.Categori_Name;
                
                mlist.Add(c);
            }
            return Redirect("Index");
        }

        public ActionResult Showproducts()
        {
           
            
            return View();
        }
        [HttpPost]
        public ActionResult Showproducts( int id)
        {
            M_Products m = new M_Products();
            List<Products> obj = p.GetProducts( id);
            List<M_Products> plist = new List<M_Products>();
            foreach (var item in obj)
            {
               
                m.Product_ID = item.Product_ID;
                m.Product_Name = item.Product_Name;
                m.Specification = item.Specification;
               m.Price = item.Price;
               m.Product_image = item.Product_image;

                plist.Add(m);
            }

            return Redirect ("Showproducts");
        }

    }
}