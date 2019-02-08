using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travel.Domain.AggregatesModel.ChargeAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class ChargeEntityTypeConfiguration : IEntityTypeConfiguration<Charge>
    {
        public void Configure(EntityTypeBuilder<Charge> builder)
        {
            builder.ToTable("Charges");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("charge_sequence");

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Date)
                .HasDefaultValue(DateTime.Now);

            builder.Property(b => b.Amount);
        }
    }
}
