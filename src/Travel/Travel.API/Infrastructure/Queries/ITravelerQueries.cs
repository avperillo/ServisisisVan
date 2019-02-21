using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.API.ViewModel;

namespace Travel.API.Infrastructure.Queries
{
    public interface ITravelerQueries
    {
        bool TravelerExists(int id);
        Task<TravelerViewModel> GetByIdAsync(int id);
        Task<IList<TravelerViewModel>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IList<TravelerViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0);
        Task<long> TravelCountAsync();
    }
}