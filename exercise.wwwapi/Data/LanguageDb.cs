using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class LanguageDb : DbContext
    {
        public LanguageDb(DbContextOptions<LanguageDb> options) : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Language>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Language>()
                .Property(p => p.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
