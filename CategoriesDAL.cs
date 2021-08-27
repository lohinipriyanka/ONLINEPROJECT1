using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BAL;


namespace DAL
{
    public class CategoriesDAL
    {
        string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";
        public List<Categories> GetCategories()
        {
            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ShowCategories]()", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //SQLDATAREADER--- read- only fwd only data
            List<Categories> categories = new List<Categories>();
            while (dr.Read())
            {
                Categories c = new Categories();
                c.Categori_ID = Convert.ToInt32(dr["Categori_ID"]);
                c.Categori_Name = dr[1].ToString();

                categories.Add(c);
            }
            cn.Close();
            cn.Dispose();
            return categories;
        }
    }
        public class ProductDal
        {
            string cnstring = "Data Source=DESKTOP-EKEQGF4;Initial Catalog=Onlie Shopping;Integrated Security=True";




            public List<Products> GetProducts()
            {

             //SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[GetCategoryData] (@Category_Categori_ID) ", cn);


            SqlConnection cn = new SqlConnection(cnstring);
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Products2]", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //SQLDATAREADER--- read- only fwd only data
                List<Products> p = new List<Products>();
                while (dr.Read())
                {
                    Products pobj = new Products();
                    pobj.Product_ID = Convert.ToInt32(dr[0]);
                    pobj.Product_Name = dr[1].ToString();
                    pobj.Specification = dr[2].ToString();
                    pobj.Price = Convert.ToInt32(dr[3]);
                    pobj.Product_image = dr[4].ToString();

                    p.Add(pobj);
                }
                cn.Close();
                cn.Dispose();
                return p;

            }

        }
    }


