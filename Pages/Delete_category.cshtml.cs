using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Ps_7.Pages
{
    public class Delete_categoryModel : PageModel
    {
        public IConfiguration _configuration { get; }
        public string lblInfoText;
        public Delete_categoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            int id_category = Int32.Parse(HttpContext.Request.Query["id"]);
            string myCompanyDB_connection_string =
_configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter productID_SqlParam = new SqlParameter("@categoryID",
           SqlDbType.Int);
            productID_SqlParam.Value = id_category;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            lblInfoText += String.Format("Deleted <b>{0}</b> record(s)<br />", numAff);
            return RedirectToPage("Category");
        }
    }
}
