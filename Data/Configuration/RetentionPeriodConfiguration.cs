using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IAR.Models;

public class RetentionPeriodConfiguration : IEntityTypeConfiguration<RetentionPeriod>
{
    public void Configure(EntityTypeBuilder<RetentionPeriod> builder)
    {
            // Set primary key to id
            builder.HasKey(t => t.ID);

            // Define relationship with Asset
            builder.HasOne(t => t.Asset)
                .WithMany(a => a.RetentionPeriods)
                .HasForeignKey(t => t.AssetID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
    }
}