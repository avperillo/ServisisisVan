using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class CollectionEntityTypeConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collections");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("collection_sequence");

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Date)
                .HasDefaultValue(DateTime.Now);

            builder.Property<int>("StateId").IsRequired();
            builder.HasOne(b => b.State)
                .WithMany()
                .HasForeignKey("StateId");

            builder.HasMany(b => b.Entries)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
