using AspSandbox.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace AspSandbox.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string UserName { get; set; }
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
            ActiveUser.updateUserName(UserName);
            ActiveUser.updatePassword(Password);
            ActiveUser.updateAge(Age);
            ActiveUser.updateGender(Gender);
            ActiveUser.updateFirstName(FirstName);
            ActiveUser.updateLastName(LastName);

            return RedirectToPage("/Index");
        }
    }
}
