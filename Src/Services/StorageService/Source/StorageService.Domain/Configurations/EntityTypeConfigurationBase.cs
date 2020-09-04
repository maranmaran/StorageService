using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageService.Domain.Entities;

namespace StorageService.Domain.Configurations
{
    public abstract class EntityTypeConfigurationBase<TEntityBase> : IEntityTypeConfiguration<TEntityBase> where TEntityBase : HierarchyHierarchyEntityBase
    {
        public void Configure(EntityTypeBuilder<TEntityBase> builder)
        {

            builder.HasIndex(x => x.HierarchyId);

            builder.Property(x => x.HierarchyId)
                .HasDefaultValue(HierarchyHelper.GetRoot());

            builder.Property(x => x.DateCreated)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(x => x.DateModified)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntityBase> builder);
    }
}