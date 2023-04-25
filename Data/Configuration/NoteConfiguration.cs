using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IAR.Models;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
            // Set primary key to id
            builder.HasKey(t => t.ID);

            // Define relationship with Asset
            builder.HasOne(t => t.Asset)
                .WithMany(a => a.Notes)
                .HasForeignKey(t => t.AssetID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
    }
}