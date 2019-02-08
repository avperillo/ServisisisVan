using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.Infrastructure.EntityTypeConfigurations
{
    class TravelerEntityTypeConfiguration : IEntityTypeConfiguration<Traveler>
    {
        public void Configure(EntityTypeBuilder<Traveler> builder)
        {
            builder.ToTable("Travelers");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ForSqlServerUseSequenceHiLo("traveler_sequence");

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Name);

            builder.Property(b => b.IsDriver);
        }
    }
}
