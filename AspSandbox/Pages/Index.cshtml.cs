using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspSandbox.Pages.Models;
using Npgsql;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Xml.Linq;

namespace AspSandbox.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
        public User ActiveUser { get; set; }
        public List<Dump> Dumps { get; set; }


        public void OnGet(int id = 0)
		{
            ViewData["id"] = id;
            ActiveUser = new User(id);
        }

        public void onPost(int dumpID, int userID)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"DELETE FROM dumps\r\nWHERE id = {dumpID}";
                postgresHelper.ExecuteNonQuery(query);
            }
        }
    }
}