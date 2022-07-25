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

        public IActionResult OnGet(int id)
        {   
            CVModel cvModel = Db.CVModels.Find(id);
            var cvModelSkills = (from s in Db.CVModelSkill where s.CVModelsId == cvModel.Id select s);
            Db.CVModelSkill.RemoveRange(cvModelSkills);
            Db.CVModels.Remove(cvModel);
            Db.AppFiles.Remove(Db.AppFiles.Find(cvModel.AppFileId));
            Db.SaveChanges();
            return RedirectToPage("BrowseCV");
        }
    }
}
