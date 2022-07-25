using asp_project.Data;
using asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_project.Pages
{
	[BindProperties]
    public class BrowseCVModel : PageModel
    {
        private ApplicationDbContext _db;
		public List<CVModel> CVModels { get; set; }
		public BrowseCVModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet()
        {
            CVModels = _db.CVModels.ToList();
        }
    }
}
