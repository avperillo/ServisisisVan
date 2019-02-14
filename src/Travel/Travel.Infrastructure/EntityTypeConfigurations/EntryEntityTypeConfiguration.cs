using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class EntryEntityTypeConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.ToTable("Entries");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("entry_sequence");

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Date)
                .HasDefaultValue(DateTime.Now);

        }
    }
}
