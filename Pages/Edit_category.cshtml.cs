using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Ps_7.Model;

namespace Ps_7.Pages
{
    public class Edit_categoryModel : PageModel
    {
        [BindProperty]
        public Category editCategory { get; set; }
        public IConfiguration _configuration { get; }
        public string lblInfoText;
        public Edit_categoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            int id_category = Int32.Parse(HttpContext.Request.Query["id"]);
            string myCompanyDB_connection_string =
           _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar,
           50);

            name_SqlParam.Value = editCategory.shortName;
            cmd.Parameters.Add(name_SqlParam);
            SqlParameter price_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar);
            price_SqlParam.Value = editCategory.longName;
            cmd.Parameters.Add(price_SqlParam);
            SqlParameter productID_SqlParam = new SqlParameter("@categoryID",
           SqlDbType.Int);
            productID_SqlParam.Value = id_category;
            //productID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            lblInfoText += String.Format("Edited <b>{0}</b> record(s)<br />", numAff);
            //int productID = (int)cmd.Parameters["@productID"].Value;
            //lblInfoText += "New ID: " + productID.ToString();
            return RedirectToPage("Category");
        }
    }
}
