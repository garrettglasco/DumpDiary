using AspSandbox.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspSandbox.Pages
{
    public class EditDumpModel : PageModel
    {
        public int Id { get; set; }
        [BindProperty]
        public string? Shape { get; set; }
        [BindProperty]
        public string? Color { get; set; }
        [BindProperty]
        public string? Amount { get; set; }
        [BindProperty]
        public string? Comments { get; set; }
        public Dump? ActiveDump { get; set; }

        public void OnGet(int id, int dumpId)
        {
            ViewData["id"] = id;
            ActiveDump = new Dump(dumpId);
        }

        public IActionResult OnPost(int id)
        {
            //int id = (int)ViewData["id"];
            ActiveDump = new Dump(1, Shape, Color, Amount, Comments);
            return RedirectToPage("/Index", new { Id = 1 });
        }
    }
}
