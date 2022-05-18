using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ps_7.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace Ps_7.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;  //
        public List<Category> categoryList = new List<Category>();
        public IConfiguration _configuration { get; }

        public CategoryModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

  

        public string lblInfoText;

        //public int id;
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
    }
}
