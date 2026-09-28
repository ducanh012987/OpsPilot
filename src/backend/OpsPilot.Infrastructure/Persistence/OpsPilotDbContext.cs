using Microsoft.EntityFrameworkCore;
using OpsPilot.Domain.Entities.Identity;
using OpsPilot.OpsPilot.Domain.Entities.Projects;

namespace OpsPilot.OpsPilot.Infrastructure.Persistence
{
    public class OpsPilotDbContext(DbContextOptions<OpsPilotDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.UserId,
                    x.RoleId
                });

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Role)
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(x => x.Token)
                    .IsUnique();

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(x => x.Code)
                    .IsUnique();

                entity.HasOne(x => x.Owner)
                    .WithMany()
                    .HasForeignKey(x => x.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.ProjectId,
                    x.UserId
                });

                entity.HasOne(x => x.Project)
                    .WithMany(x => x.Members)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
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
        public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
    }
}
