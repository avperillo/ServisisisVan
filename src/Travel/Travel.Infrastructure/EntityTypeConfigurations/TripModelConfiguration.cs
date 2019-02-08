using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travel.Domain.AggregatesModel.TravelAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trips");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ForSqlServerUseSequenceHiLo("trip_sequence");

            builder.Ignore(t => t.DomainEvents);

            builder.Property(t => t.Date)
                .HasDefaultValue(DateTime.Now);

            builder.Property(t => t.Commentary);
        }
    }
}
