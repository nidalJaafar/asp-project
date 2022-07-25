using asp_project.Data;
using asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_project.Pages
{
	[BindProperties]
    public class SummaryModel : PageModel
    {
        private ApplicationDbContext _db;

		public SummaryModel(ApplicationDbContext db)
		{
			_db = db;
        }

        public CVModel CVModel { get; set; }
        public List<Skill> Skills { get; set; }

		public void OnGet(int id)
        {
            CVModel = _db.CVModels.Find(id);
            CVModel.Nationality = (from x in _db.Nationalities where x.Id == CVModel.NationalityId select x).First();
            CVModel.Skills = (from skill in _db.Skills from cvskill in _db.CVModelSkill where (cvskill.CVModelsId == id && cvskill.SkillsId == skill.Id) select skill).ToList();
        }
    }
}
