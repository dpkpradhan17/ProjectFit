using Microsoft.EntityFrameworkCore;
using ProjectFit.Entities;

namespace ProjectFit
{
    public class ProjectFitContext : DbContext
    {
        public ProjectFitContext(DbContextOptions<ProjectFitContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Coaches> Coaches { get; set; }

        public DbSet<Plans> Plans { get; set; }
    }
}
