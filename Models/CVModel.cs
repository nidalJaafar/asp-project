using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_project.Models
{
    [BindProperties]
    public class CVModel
    {
        public int Id { get; set; }
		[Display(Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        public Nationality? Nationality { get; set; }
        public int NationalityId { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public int? Grade { get; set; }
        public int AppFileId { get; set; }
        public AppFile? AppFile { get; set; }
		
    }
}
