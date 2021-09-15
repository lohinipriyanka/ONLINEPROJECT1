using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        Logins U = new Logins();
        ProductDal dal = new ProductDal();
        // GET: Main1
        public ActionResult FindCategory()
        {
            return View();
        }
       
        public ActionResult Showproducts(int id)
        {
            List<M_Products> p1 = new List<M_Products>();
            List<Products> data = dal.GetProducts(id);
            foreach (var item in data)
            {
                M_Products c = new M_Products();
                c.Product_ID = item.Product_ID;
                c.Product_Name = item.Product_Name;
                c.Specification = item.Specification;
                c.Price = item.Price;
                c.Product_image = item.Product_image;
                p1.Add(c);
            }
            
            return View(p1);
        }
        public ActionResult LoginDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginDetails(int id, string name,string password)
        {
           
            Logins U1 = new Logins();
            U.UserID = id;
            Session["userid"] = U.UserID;
            U.User_Name = name;
            U.User_Password = password;
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "Select * from UserDetails where @User_ID =User_ID";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("@User_ID", U.UserID);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    U1.User_Name = dr["User_Name"].ToString();
                    U1.User_Password = dr["User_Password"].ToString();
                    if (U1.User_Name == U.User_Name && U1.User_Password == U.User_Password)
                    {
                        return RedirectToAction("FindCategory");
                    }
                    
                }    
            }

            con.Close();
            Response.Write("User not Found");
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(int id, string name, string password, string Email)
        {
            Logins U = new Logins();
            U.UserID = id;
            U.User_Name = name;
            U.User_Password = password;
            U.User_EmailID = Email;
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "insert into UserDetails(User_ID,User_Name, User_Password, User_EmailID)values(@User_ID, @User_Name, @User_Password,@User_EmailID)";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("@User_ID", U.UserID);
            cmd.Parameters.AddWithValue("@User_Name", U.User_Name);
            cmd.Parameters.AddWithValue("@User_Password", U.User_Password);
            cmd.Parameters.AddWithValue("@User_EmailID", U.User_EmailID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("LoginDetails");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword( int id,string password)
        {
            Logins U = new Logins();
            U.User_Password = password;
            U.UserID = id;
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "UPDATE UserDetails SET  User_Password=@User_Password WHERE User_ID=@User_ID";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("@User_ID", U.UserID);
            cmd.Parameters.AddWithValue("@User_Password", U.User_Password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("LoginDetails");
        }
      
        public ActionResult Details(int id)
        {

            Products data = dal.GetProduct(id);
            M_Products c = new M_Products();
            c.Product_ID = data.Product_ID;
            c.Product_Name = data.Product_Name;
            c.Specification = data.Specification;
            c.Price = data.Price;
            c.Product_image = data.Product_image;
            Session["key"] = c.Product_ID;
            return View(c);
        }
        public ActionResult cart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cart(int Quantity)
        {
            M_Products M = new M_Products();
            M.Product_ID =Convert.ToInt32(Session["key"]);
            M.Quantity = Quantity;
            Products data = dal.GetProduct(M.Product_ID);
            string cnstring2 = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con2 = new SqlConnection(cnstring2);
            string insertquery2 = "Select * from Suppliers where Product_ID=@Product_ID";
            SqlCommand cmd2 = new SqlCommand(insertquery2, con2);
            cmd2.Parameters.AddWithValue("@Product_ID", M.Product_ID);
            con2.Open();
            SqlDataReader dr1=cmd2.ExecuteReader();
            M_Products r = new M_Products();
            while (dr1.Read())
            {
             
                r.Quantity = Convert.ToInt32(dr1["P_Quantity"]);

            }
            con2.Close();
            if (M.Quantity <= r.Quantity)
            {
                string cnstring1 = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
                SqlConnection con = new SqlConnection(cnstring1);
                string insertquery = "insert into Carts(UserDetail_User_ID,Product1_ID,Product_Name," +
                    "Specification,Price,Product_Image,Quantity)values" +
               "(@UserDetail_User_ID,@Product1_ID,@Product_Name,@Specification,@Price,@Product_Image,@Quantity)";
                SqlCommand cmd = new SqlCommand(insertquery, con);
                cmd.Parameters.AddWithValue("@UserDetail_User_ID", Session["userid"]);
                cmd.Parameters.AddWithValue("@Quantity", M.Quantity);
                cmd.Parameters.AddWithValue("@Product1_ID", M.Product_ID);
                cmd.Parameters.AddWithValue("@Product_Name", data.Product_Name);
                cmd.Parameters.AddWithValue("@Specification", data.Specification);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Product_Image", data.Product_image);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("CartProducts");
            }
            else
            {
                ViewBag.Error = "out of Stock";
                return View();
            }

        }
        public ActionResult CartProducts()
        {
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con1 = new SqlConnection(cnstring);
            string insertquery1 = "Select * from Carts where UserDetail_User_ID=@UserDetail_User_ID";
            SqlCommand cmd1 = new SqlCommand(insertquery1, con1);
            cmd1.Parameters.AddWithValue("@UserDetail_User_ID", Session["userid"]);
            con1.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            //SQLDATAREADER--- read- only fwd only data
            List<M_Products> L = new List<M_Products>();
            while (dr.Read())
            {
                M_Products pobj = new M_Products();
                pobj.User_ID = Convert.ToInt32(dr["UserDetail_User_ID"]);
                pobj.Product_Name = dr["Product_Name"].ToString();
                pobj.Specification = dr["Specification"].ToString();
                pobj.Price = Convert.ToInt32(dr["Price"]);
                pobj.Product_image = dr["Product_image"].ToString();
                pobj.Product_ID = Convert.ToInt32(dr["Product1_ID"]);
                pobj.Quantity = Convert.ToInt32(dr["Quantity"]);
                L.Add(pobj);
            }
            con1.Close();
           // ViewBag.error = Session["error"];
            return View(L);
        }
        public ActionResult Buy()
        {
           string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "Select * from carts where UserDetail_User_ID=@User_ID";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("@User_ID",Session["userid"]);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<M_Products> m = new List<M_Products>();
            int sum = 0;
            while (dr.Read())
            {
                M_Products pobj = new M_Products();
                pobj.User_name = dr["UserDetail_User_ID"].ToString();
                pobj.Product_ID = Convert.ToInt32(dr["Product1_ID"]);
                pobj.Product_Name = dr["Product_Name"].ToString();
                pobj.Specification = dr["Specification"].ToString();
                pobj.Quantity = Convert.ToInt32(dr["Quantity"]);
                pobj.Price = Convert.ToInt32(dr["Price"]);
                m.Add(pobj);
                sum += (pobj.Quantity * pobj.Price);
                
            }
            if (sum > 0)
            {
                ViewBag.totalprice = sum;
                Session["tp"] = sum;
                return View(m);
            }
            else
            {
               
                Session["error"] = "Cart is Empty. Add atleast one Item to proceed" ;
                return RedirectToAction("CartProducts");
            }
        }
        public ActionResult Payment()
        {
            ViewBag.tp = Session["tp"];
            return View();
        }
        [HttpPost]
        public ActionResult Payment(int id)
        {
            
            return RedirectToAction("EndPage");
        }
        public ActionResult SupplierLogin()
        {


            return View();
        }
        [HttpPost]
        public ActionResult SupplierLogin(string User_Name, string Password)
        {
            Logins U1 = new Logins();
            Logins U2 = new Logins();
            U2.User_Name = User_Name;
            U2.User_Password = Password;
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "Select * from Supplier_Login ";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    U1.User_Name = dr["Supplier_Username"].ToString();
                    U1.User_Password = dr["S_Password"].ToString();
                    if (U1.User_Name == U2.User_Name && U1.User_Password == U2.User_Password)
                    {
                        return RedirectToAction("Supplier_Category");
                    }

                }
            }
            con.Close();
            Response.Write("User not Found");
            return View();
        }
       
        public ActionResult Supplier_Products(int id)
        {
            List<M_Products> p1 = new List<M_Products>();
            List<Products> data = dal.Supplier_Products(id);
            Session["cid"] = id;
            foreach (var item in data)
            {
                M_Products c = new M_Products();
                //c.Product_ID = item.Product_ID;
                c.Product_Name = item.Product_Name;
                c.Specification = item.Specification;
                c.Price = item.Price;
                c.Product_image = item.Product_image;
                c.Category_Categori_ID = item.Category_Categori_ID;
                p1.Add(c);
            }
            return View(p1);
            
        }

        public ActionResult Supplier_Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Supplier_Create(FormCollection C)
        {
            M_Products P = new M_Products();
            P.Product_Name = Request.Form["Product_Name"];
            P.Price = Convert.ToInt32(Request.Form["Price"]);
            P.Specification= Request.Form["Specification"];
            P.Product_image = Request.Form["Product_Image"];
            P.Quantity = Convert.ToInt32(Request.Form["Quantity"]) ;
            
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con2 = new SqlConnection(cnstring);
            string insertquery2 = "select * from Products1";
            SqlCommand cmd2 = new SqlCommand(insertquery2, con2);
            //cmd2.Parameters.AddWithValue("@Category_Categori_ID",Session["Catid"]);
            con2.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            M_Products m1 = new M_Products();
            while (dr.Read())
            {

                m1.Product_ID = Convert.ToInt32(dr["Product_ID"]);

            }
            Session["mainid"] = m1.Product_ID;
            con2.Close();
            SqlConnection con = new SqlConnection(cnstring);
           string insertquery = "insert into Suppliers(Product_ID,Product_Name," +
                "Specification,Price,Product_Image,Category_Categori_ID,P_Quantity)values" +
           "(@Product_ID,@Product_Name,@Specification,@Price,@Product_Image," +
           "@Category_Categori_ID,@P_Quantity)";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("Product_ID",Convert.ToInt32(Session["mainid"])+1);
            cmd.Parameters.AddWithValue("@Product_Name",P.Product_Name);
            cmd.Parameters.AddWithValue("@Specification",P.Specification);
            cmd.Parameters.AddWithValue("@Price", P.Price);
            cmd.Parameters.AddWithValue("@P_Quantity", P.Quantity);
            cmd.Parameters.AddWithValue("@Product_Image", P.Product_image);
            cmd.Parameters.AddWithValue("@Category_Categori_ID",Session["cid"]);
            ViewBag.catid = Session["cid"];
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
            M_Products P1 = new M_Products();
            SqlConnection con1 = new SqlConnection(cnstring);
            string insertquery1 = "insert into Products1(Product_ID,Product_Name,Specification," +
                "Price,Product_Image,Category_Categori_ID)values" +
           "(@Product_ID,@Product_Name,@Specification,@Price,@Product_Image,@Category_Categori_ID)";
           
            SqlCommand cmd1 = new SqlCommand(insertquery1, con1);
            Session["mainid1"] = Convert.ToInt32(Session["mainid"]) + 1;
            cmd1.Parameters.AddWithValue("Product_ID", Session["mainid1"]);
            cmd1.Parameters.AddWithValue("@Product_Name", P.Product_Name);
            cmd1.Parameters.AddWithValue("@Specification", P.Specification);
            cmd1.Parameters.AddWithValue("@Price", P.Price);
            cmd1.Parameters.AddWithValue("@Product_Image", P.Product_image);
            cmd1.Parameters.AddWithValue("@Category_Categori_ID", Session["cid"]);
            
            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();
           
            return View();
        }
        public ActionResult Supplier_Category()
        {

            return View();
        }
        public ActionResult Delete(int id)
        {
            M_Products P = new M_Products();
            P.Product_ID = id;
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
            SqlConnection con = new SqlConnection(cnstring);
            string insertquery = "Delete from Carts where  Product1_ID=@Product_ID";
            SqlCommand cmd = new SqlCommand(insertquery, con);
            cmd.Parameters.AddWithValue("@Product_ID", P.Product_ID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Cart = Session["userid"];

            return View();
        }
        public ActionResult EndPage()
        {
            return View();
        }
        
        




    }
   
}