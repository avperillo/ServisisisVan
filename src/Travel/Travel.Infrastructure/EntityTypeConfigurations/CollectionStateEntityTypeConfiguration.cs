using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class CollectionStateEntityTypeConfiguration : IEntityTypeConfiguration<CollectionState>
    {
        public void Configure(EntityTypeBuilder<CollectionState> builder)
        {
            builder.ToTable("CollectionStates");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}