using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IAR.Models;

public class ThirdPartyConfiguration : IEntityTypeConfiguration<ThirdParty>
{
    public void Configure(EntityTypeBuilder<ThirdParty> builder)
    {
            // Set primary key to id
            builder.HasKey(t => t.ID);

            // Define relationship with Asset
            builder.HasOne(t => t.Asset)
                .WithMany(a => a.ThirdParties)
                .HasForeignKey(t => t.AssetID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
    }
}