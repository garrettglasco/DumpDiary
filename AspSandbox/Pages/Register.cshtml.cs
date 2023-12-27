using AspSandbox.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace AspSandbox.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public int Age { get; set; }
        [BindProperty]
        public string Gender { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        public User ActiveUser { get; set; }

        public void OnGet(int id)
        {
            ViewData["id"] = id;
            ActiveUser = new User(id);
        }

        public IActionResult OnPost()
        {
            if (IsValidLogin(Username, Password))
            {
                //TempData["UserId"] = getUserId(Username, Password);
                return RedirectToPage("/Lgoin", new { Id = getUserId(Username, Password) });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }
        private bool IsValidLogin(string username, string password)
        {
            string DUsername = null;
            string DPassword = null;
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"SELECT id, username, password\r\nFROM users\r\nWHERE username = '{username}'\r\nAND password = '{password}'";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        DUsername = reader.GetString(1);
                        DPassword = reader.GetString(2);
                    }
                }
            }
            return !string.IsNullOrEmpty(DUsername) && !string.IsNullOrEmpty(DPassword);
        }

        private int getUserId(string username, string password)
        {
            string DUsername = null;
            string DPassword = null;
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"SELECT id\r\nFROM users\r\nWHERE username = '{username}'\r\nAND password = '{password}'";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                    }
                }
            }
            return Id;
        }
    }
}
