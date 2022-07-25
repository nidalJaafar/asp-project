using System.ComponentModel.DataAnnotations;

namespace asp_project.Models
{
	public class CVModelSkill
	{
		public int CVModelsId { get; set; }
		public CVModel CVModel { get; set; }
		public int SkillsId { get; set; }
		public Skill Skill { get; set; }

	}
}
