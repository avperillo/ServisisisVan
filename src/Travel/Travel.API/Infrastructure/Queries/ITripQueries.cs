using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.API.ViewModel;

namespace Travel.API.Infrastructure.Queries
{
    public interface ITripQueries
    {
        Task<TripViewModel> GetTripByIdAsync(int id);
        Task<IList<TripViewModel>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IList<TripViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0);
        Task<long> CountAsync();
    }
}