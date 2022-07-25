using System.ComponentModel.DataAnnotations;

namespace asp_project.Models
{
    public class Nationality
    {
		public int Id { get; set; }
        public string Name { get; set; }

		public Nationality(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
