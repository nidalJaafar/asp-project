using asp_project.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_project.Pages
{
    public class BrowseCVModel : PageModel
    {
        public ApplicationDbContext Db { get; set; }
        public BrowseCVModel(ApplicationDbContext db)
        {
            Db = db;
        }

    }
}
