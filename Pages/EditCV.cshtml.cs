using asp_project.Models;
using asp_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace asp_project.Pages
{
    [BindProperties]
    public class EditCVModel : PageModel
    {
        [BindNever]
        public ApplicationDbContext Db { get; set; }

        public CVModel CVModel { get; set; }

        [Range(1, 20, ErrorMessage = "X should be between 1 and 20")]
        public int X { get; set; }

        [Range(20, 50, ErrorMessage = "Y should be between 1 and 20")]
        public int Y { get; set; }
        public int Sum { get; set; }
        public string ConfirmationEmail { get; set; }
        public IFormFile Image { get; set; }
        public EditCVModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public void OnGet(int id)
        {
            CVModel = Db.CVModels.Find(id);
        }

        public IActionResult OnPost(int[] skillId)
        {
            var cvModelSkills = (from s in Db.CVModelSkill where s.CVModelsId == CVModel.Id select s);
            Db.CVModelSkill.RemoveRange(cvModelSkills);
            Db.SaveChanges();

            int appFileId = CVModel.AppFileId;

            MemoryStream memoryStream = new MemoryStream();
            Image.CopyTo(memoryStream);

            if (memoryStream.Length < 20971520)
            {
                AppFile file = new AppFile()
                {
                    Data = memoryStream.ToArray(),
                    ContentType = Image.ContentType,
                    Name = Image.Name
                };

                Db.AppFiles.Add(file);
                Db.SaveChanges();
                CVModel.AppFileId = file.Id;
            }

            int Grade = 0;

            if (!CVModel.Email.Equals(ConfirmationEmail))
            {
                ModelState.AddModelError("CVModel.Email", "The confirmation email does not match");
            }
            if (X + Y != Sum)
            {
                ModelState.AddModelError("CVModel.Sum", "Sum is not equal to X + Y");
            }
            if (skillId.Length == 0)
            {
                ModelState.AddModelError("CVModel.Skills", "Skills are required");
            }
            if (ModelState.IsValid)
            {
                List<Skill> skills = new List<Skill>();
                foreach (var id in skillId)
                {
                    Grade += 10;
                    skills.Add(Db.Skills.Find(id));
                }
                CVModel.Skills = skills;
                CVModel.Grade = Grade + (CVModel.Gender.Equals("male") ? 5 : 10);
                Db.CVModels.Update(CVModel);
                Db.SaveChanges();
                Db.AppFiles.Remove(Db.AppFiles.Find(appFileId));
                Db.SaveChanges();
                return RedirectToPage("SummaryCV", new { id = CVModel.Id });
            }
            return Page();
        }
    }
}
