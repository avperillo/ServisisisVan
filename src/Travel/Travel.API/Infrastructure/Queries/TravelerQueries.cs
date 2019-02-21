using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Infrastructure;

namespace Travel.API.Infrastructure.Queries
{
    public class TravelerQueries : ITravelerQueries
    {
        private readonly TravelContext _context;
        private readonly IMapperService _mapper;

        public TravelerQueries(TravelContext context, IMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TravelerViewModel> GetByIdAsync(int id)
        {
            var qry = await _context.Travelers.FindAsync(id);
            return _mapper.Map<TravelerViewModel>(qry);
        }
        public async Task<IList<TravelerViewModel>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var qry = await _context.Travelers.Where(ci => ids.Contains(ci.Id)).ToListAsync();
            return _mapper.Map<IList<TravelerViewModel>>(qry);
        }
        public async Task<IList<TravelerViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0)
        {
            var qry = await _context.Travelers
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IList<TravelerViewModel>>(qry);
        }
        public async Task<long> TravelCountAsync()
        {
            return await _context.Travelers.LongCountAsync();
        }
        public bool TravelerExists(int id)
        {
            return _context.Travelers.Any(t => t.Id == id);
        }

    }
}
