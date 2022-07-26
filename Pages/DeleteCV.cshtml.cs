using asp_project.Data;
using asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_project.Pages
{
    public class DeleteCVModel : PageModel
    {
        public ApplicationDbContext Db { get; set; }

        public DeleteCVModel(ApplicationDbContext db)
        {
            Db = db;
        }

        [BindProperty]
        public CVModel CVModel { get; set; }

        public void OnGet(int id)
        {
            CVModel = Db.CVModels.Find(id);
        }

        public IActionResult OnPost()
        {
            var cvModelSkills = (from s in Db.CVModelSkill where s.CVModelsId == CVModel.Id select s);
            Db.CVModelSkill.RemoveRange(cvModelSkills);
            Db.CVModels.Remove(CVModel);
            Db.AppFiles.Remove(Db.AppFiles.Find(CVModel.AppFileId));
            Db.SaveChanges();
            return RedirectToPage("/BrowseCV");
        }
    }
}
