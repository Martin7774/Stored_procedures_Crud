using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ps_7.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Product newProduct { get; set; }
        public List<Category> categoryList = new List<Category>();
        public IConfiguration _configuration { get; }
        public string lblInfoText;
        public AddModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //StringBuilder htmlStr = new StringBuilder("");
            while (reader.Read())
            {
                categoryList.Add(new Category
                {
                    id = Int32.Parse(reader["Id"].ToString()),
                    shortName = reader.GetString(1),
                    longName = reader.GetString(2)
                });
            }

            reader.Close();
            con.Close();

        }
        public IActionResult OnPost()
        {
            string myCompanyDB_connection_string =
           _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_productAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.VarChar,
           50);
            
            name_SqlParam.Value = newProduct.name;
            cmd.Parameters.Add(name_SqlParam);
            SqlParameter price_SqlParam = new SqlParameter("@price", SqlDbType.Money);
            price_SqlParam.Value = newProduct.price;
            cmd.Parameters.Add(price_SqlParam);
            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID",
           SqlDbType.Int);
            categoryID_SqlParam.Value = newProduct.categoryId;
            cmd.Parameters.Add(categoryID_SqlParam);
            SqlParameter productID_SqlParam = new SqlParameter("@productID",
           SqlDbType.Int);
            productID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            lblInfoText += String.Format("Inserted <b>{0}</b> record(s)<br />", numAff);
            int productID = (int)cmd.Parameters["@productID"].Value;
            lblInfoText += "New ID: " + productID.ToString();
            return RedirectToPage("Index");
        }
    }
}
