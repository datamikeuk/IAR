using Microsoft.EntityFrameworkCore;
using IAR.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IAR.Data
{
    public class RegisterContext : DbContext
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
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Role>()
            //     .HasNoKey();
            // modelBuilder.Entity<User>()
            //     .HasNoKey();
            modelBuilder.Entity<ThirdParty>()
                .ToTable("ThirdParty")
                .HasOne(t => t.Asset)
                .WithMany(a => a.ThirdParties)
                .HasForeignKey(t => t.AssetID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false)
                ;

            modelBuilder.Entity<BackEndPlatform>()
                .ToTable("BackEndPlatform")
                .HasMany(p => p.Assets)
                .WithOne(a => a.BackEndPlatform)
                ;

            modelBuilder.Entity<FrontEndPlatform>()
                .ToTable("FrontEndPlatform")
                .HasMany(p => p.Assets)
                .WithOne(a => a.FrontEndPlatform)
                ;

            modelBuilder.Entity<Asset>()
                .ToTable("Asset")
                .HasMany(a => a.ThirdParties)
                .WithOne(t => t.Asset)
                .IsRequired(false)
                ;

            modelBuilder.Entity<Asset>()
                .ToTable("Asset")
                .HasOne(a => a.BackEndPlatform)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.BackEndPlatformID)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false)
                ;
                
            modelBuilder.Entity<Asset>()
                .ToTable("Asset")
                .HasOne(a => a.FrontEndPlatform)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.FrontEndPlatformID)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false)
                ;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userID = _userResolver.GetIdentityName();

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