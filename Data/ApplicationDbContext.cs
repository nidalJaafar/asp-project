using asp_project.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<CVModel> CVModels { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CVModelSkill> CVModelSkill { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CVModelSkill>()
                .HasKey(k => new { k.CVModelsId, k.SkillsId });
            modelBuilder.Entity<CVModel>()
                .HasMany<Skill>(s => s.Skills)
                .WithMany(c => c.CVModels)
                .UsingEntity<CVModelSkill>();
            modelBuilder.Entity<CVModel>()
                .HasOne(c => c.AppFile)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Nationality>()
                .HasData
                (
                    new Nationality(1, "Lebanese"),
                    new Nationality(2, "American"),
                    new Nationality(3, "German"),
                    new Nationality(4, "French"),
                    new Nationality(5, "Italian"),
                    new Nationality(6, "Russian"),
                    new Nationality(7, "Scottish"),
                    new Nationality(8, "British")
                );
            modelBuilder.Entity<Skill>()
                .HasData
                (
                    new Skill(1, "Java"),
                    new Skill(2, "C"),
                    new Skill(3, "Python"),
                    new Skill(4, "C++"),
                    new Skill(5, "C#"),
                    new Skill(6, "JavaScript"),
                    new Skill(7, "TypeScript"),
                    new Skill(8, "PHP")
                );
        }
    }
}
