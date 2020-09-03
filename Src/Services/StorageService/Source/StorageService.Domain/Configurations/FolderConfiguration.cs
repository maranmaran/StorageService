using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageService.Domain.Entities;

namespace StorageService.Domain.Configurations
{
    public class FolderConfiguration : EntityTypeConfigurationBase<Folder>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Folder> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder.HasIndex(x => x.ParentFolderId);

            builder
                .HasMany(x => x.Files)
                .WithOne(x => x.ParentFolder)
                .HasForeignKey(x => x.ParentFolderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Folders)
                .WithOne(x => x.ParentFolder)
                .HasForeignKey(x => x.ParentFolderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.ParentFolder)
                .WithMany(x => x.Folders)
                .HasForeignKey(x => x.ParentFolderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
