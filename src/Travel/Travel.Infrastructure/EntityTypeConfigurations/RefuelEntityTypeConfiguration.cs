using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class RefuelEntityTypeConfiguration : IEntityTypeConfiguration<Refuel>
    {
        public void Configure(EntityTypeBuilder<Refuel> builder)
        {
            builder.ToTable("Refuels");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("refuel_sequence");

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Date)
                .HasDefaultValue(DateTime.Now);

            builder.Property(b => b.Amount);
        }
    }
}
