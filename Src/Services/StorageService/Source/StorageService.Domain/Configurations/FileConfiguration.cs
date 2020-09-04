using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageService.Domain.Entities;

namespace StorageService.Domain.Configurations
{
    public class FileConfiguration : EntityTypeConfigurationBase<File>
    {
        public override void ConfigureEntity(EntityTypeBuilder<File> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder.HasIndex(x => new { x.Name, x.ParentFolderId });

            builder
                .HasOne(x => x.ParentFolder)
                .WithMany(x => x.Files)
                .OnDelete(DeleteBehavior.SetNull);
        }    
    }
}