using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class StudentDb : DbContext
    {
        public StudentDb(DbContextOptions<StudentDb> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>()
                .Property(p => p.FirstName)
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<Student>()
                .Property(p => p.LastName)
                .HasColumnType("varchar(255)");
        }
    }
}
