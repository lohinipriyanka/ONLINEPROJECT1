using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineshopping.Models
{
    public class M_Products
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Specification{  get; set; }
        public int Price { get; set; }
        public string Product_image { get; set; }
        public  int Category_Categori_ID{ get; set; }


    }
}