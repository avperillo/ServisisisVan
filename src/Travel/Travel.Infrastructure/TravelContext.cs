using Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Travel.Infrastructure.EntityTypeConfigurations;

namespace Travel.Infrastructure
{
    public class TravelContext : DbContext, IUnitOfWork
    {

        public DbSet<Travel.Domain.AggregatesModel.TravelAggregate.Trip> Travels { get; set; }
        public DbSet<Travel.Domain.AggregatesModel.TravelerAggregate.Traveler> Travelers { get; set; }
        public DbSet<Travel.Domain.AggregatesModel.RefuelAggregate.Refuel> Refuels { get; set; }
        public IDbContextTransaction CurrentTransaction { get; private set; }
        
        public TravelContext(DbContextOptions<TravelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TripEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TravelerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChargeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefuelEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }

        public async Task BeginTransactionAsync()
        {
            CurrentTransaction = CurrentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                CurrentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                CurrentTransaction?.Rollback();
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }
    }
}
