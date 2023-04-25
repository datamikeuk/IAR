using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IAR.Models;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
            builder.HasMany(a => a.ThirdParties)
                .WithOne(t => t.Asset)
                .IsRequired(false);

            builder.HasMany(a => a.Notes)
                .WithOne(t => t.Asset)
                .IsRequired(false);

            builder.HasMany(a => a.RetentionPeriods)
                .WithOne(t => t.Asset)
                .IsRequired(false);                                

            builder.HasOne(a => a.BackEndPlatform)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.BackEndPlatformID)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
                
            builder.HasOne(a => a.FrontEndPlatform)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.FrontEndPlatformID)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(a => a.ExecutiveSponsor)
                .WithMany(u => u.ExecutiveSponsorAssets)
                .HasForeignKey(a => a.ExecutiveSponsorAccountName)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(a => a.DataOwner)
                .WithMany(u => u.DataOwnerAssets)
                .HasForeignKey(a => a.DataOwnerAccountName)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(a => a.DataSteward)
                .WithMany(u => u.DataStewardAssets)
                .HasForeignKey(a => a.DataStewardAccountName)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
    }
}