using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ps_7.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ps_7.Pages
{
    public class Add_categoryModel : PageModel
    {
        [BindProperty]
        public Category newCategory { get; set; }
        public IConfiguration _configuration { get; }
        public string lblInfoText;
        public Add_categoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            string myCompanyDB_connection_string =
           _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar,
           50);

            name_SqlParam.Value = newCategory.shortName;
            cmd.Parameters.Add(name_SqlParam);
            SqlParameter price_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar);
            price_SqlParam.Value = newCategory.longName;
            cmd.Parameters.Add(price_SqlParam);
            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID",
           SqlDbType.Int);
            categoryID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(categoryID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("Category");
        }
    }
}
