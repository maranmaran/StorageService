using Microsoft.EntityFrameworkCore;
using StorageService.Domain.Entities;
using StorageService.Domain.Seed;
using File = StorageService.Domain.Entities.File;

namespace StorageService.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed(); // seeds database with data..

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
