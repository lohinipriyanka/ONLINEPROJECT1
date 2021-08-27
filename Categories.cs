using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BAL
{
    public class Categories
    {
        public int Categori_ID { get; set; }
        public string Categori_Name { get; set; }

    }
    public class Products
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Specification { get; set; }
        public int Price { get; set; }
        public string Product_image { get; set; }
    }
    public class UserDetails
    {
        public int user_ID { get; set; }
        public string user_Name { get; set; }
        public string password { get; set; }
        public string City { get; set; }
        public int pincode{ get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class Supplier
    {
        public int Supplier_ID { get; set; }
        public string  Supplier_Name { get; set; }
        public string password  { get; set; }
    }
    public class Admin
    {
        public int Admin_ID { get; set; }
        public int Admin_password { get; set; }

    }
    public class Cart_Details
    {
        public int Quantity { get; set; }
        public int Cart_ID { get; set; }
        public int User_ID { get; set; }

    }
    
}
