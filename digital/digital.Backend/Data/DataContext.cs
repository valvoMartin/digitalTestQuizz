using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }



        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<Sector> Sectors { get; set; }



        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerUser> AnswersUsers { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();

            modelBuilder.Entity<Company>().HasIndex(c => new { c.Cuit }).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => new { c.Name }).IsUnique();

            modelBuilder.Entity<Question>().HasIndex(c => new { c.Text }).IsUnique();
            modelBuilder.Entity<Answer>().HasIndex(c => new { c.Text }).IsUnique();

            modelBuilder.Entity<AnswerUser>().HasKey(au => new { au.Email, au.QuestionId, au.AnswerId });






            DisableCascadingDelete(modelBuilder);

        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


    }
}
