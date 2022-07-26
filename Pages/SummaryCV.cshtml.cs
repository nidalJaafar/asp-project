using asp_project.Data;
using asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace asp_project.Pages
{
    [BindProperties]
    public class SummaryModel : PageModel
    {
        [BindNever]
        public ApplicationDbContext Db { get; set; }

        public string ImageSrc { get; set; }

        public SummaryModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public CVModel CVModel { get; set; }

        public void OnGet(int id)
        {

            CVModel = Db.CVModels.Find(id);
            CVModel.Nationality = (from x in Db.Nationalities where x.Id == CVModel.NationalityId select x).First();
            CVModel.Skills = (from skill in Db.Skills from cvskill in Db.CVModelSkill where (cvskill.CVModelsId == id && cvskill.SkillsId == skill.Id) select skill).ToList();
            AppFile appFile = Db.AppFiles.Find(CVModel.AppFileId);

            ImageSrc = "data:" + appFile.ContentType + ";base64," + Convert.ToBase64String(appFile.Data, 0, appFile.Data.Length);
        }
    }
}
