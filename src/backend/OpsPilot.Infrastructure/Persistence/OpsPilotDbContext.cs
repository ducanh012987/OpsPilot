using Microsoft.EntityFrameworkCore;
using OpsPilot.OpsPilot.Domain.Entities.Projects;

namespace OpsPilot.OpsPilot.Infrastructure.Persistence
{
    public class OpsPilotDbContext(DbContextOptions<OpsPilotDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Implement any custom logic before saving changes
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<Project> Projects => Set<Project>();
    }
}
