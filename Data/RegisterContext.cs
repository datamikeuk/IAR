using Microsoft.EntityFrameworkCore;
using IAR.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Audit.EntityFramework;
using Audit.Core;

namespace IAR.Data
{
    public class RegisterContext : AuditDbContext
    {
        private readonly UserResolver _userResolver;
        public RegisterContext (DbContextOptions<RegisterContext> options, UserResolver userResolver) : base(options)
        {
            _userResolver = userResolver;
        }

        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<BackEndPlatform> BackEndPlatforms => Set<BackEndPlatform>();
        public DbSet<FrontEndPlatform> FrontEndPlatforms => Set<FrontEndPlatform>();
        public DbSet<ThirdParty> ThirdParties => Set<ThirdParty>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<RetentionPeriod> RetentionPeriods => Set<RetentionPeriod>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<AuditLog> AuditLog => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new ThirdPartyConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<BackEndPlatform>()
                .HasMany(p => p.Assets)
                .WithOne(a => a.BackEndPlatform);

            modelBuilder.Entity<FrontEndPlatform>()
                .HasMany(p => p.Assets)
                .WithOne(a => a.FrontEndPlatform);
            }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userID = _userResolver.GetUserName();

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Where(e => e.Entity is Asset)
                ;

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                entityEntry.Property("UpdatedBy").CurrentValue = userID;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    entityEntry.Property("CreatedBy").CurrentValue = userID;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}