using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Infrastructure;

namespace Travel.API.Infrastructure.Queries
{
    public class RefuelQueries : IRefuelQueries
    {
        private readonly TravelContext _context;
        private readonly IMapperService _mapper;

        public RefuelQueries(TravelContext context, IMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RefuelViewModel> GetByIdAsync(int id)
        {
            var qry = await _context.Refuels.FindAsync(id);
            return _mapper.Map<RefuelViewModel>(qry);
        }

        public async Task<IList<RefuelViewModel>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var qry = await _context.Refuels.Where(ci => ids.Contains(ci.Id)).ToListAsync();
            return _mapper.Map<IList<RefuelViewModel>>(qry);
        }

        public async Task<IList<RefuelViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0)
        {
            var qry = await _context.Refuels
                .OrderBy(c => c.Date)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IList<RefuelViewModel>>(qry);
        }

        public async Task<long> RefuelCountAsync()
        {
            return await _context.Refuels.LongCountAsync();
        }


    }
}
