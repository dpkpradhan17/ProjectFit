using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectFit.Entities;

namespace ProjectFit
{
    public class ProjectFitContext : IdentityDbContext<User>
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

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectFitContext>
    {
        public ProjectFitContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ProjectFitContext>();
            var connectionString = configuration.GetConnectionString("SqlCon");
            builder.UseSqlServer(connectionString);
            return new ProjectFitContext(builder.Options);
        }
    }
}
