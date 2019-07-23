using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Infrastructure.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly TravelContext context;

        public IUnitOfWork UnitOfWork => context;

        public CollectionRepository(TravelContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Collection Add(Collection collection)
        {
            return context.Collections.Add(collection).Entity;
        }

        public async Task<Collection> GetByIdAsync(int idCollection)
        {
            Collection collection = await context.Collections.FindAsync(idCollection);

            if (collection != null)
            {
                await context.Entry(collection)
                    .Collection(x => x.Entries).LoadAsync();
                await context.Entry(collection)
                    .Reference(i => i.State).LoadAsync();
            }

            return collection;
        }

        public void Update(Collection collection)
        {
            context.Entry(collection).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
