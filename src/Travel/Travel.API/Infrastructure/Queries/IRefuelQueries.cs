using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.API.ViewModel;

namespace Travel.API.Infrastructure.Queries
{
    public interface IRefuelQueries
    {
        Task<IList<RefuelViewModel>> GetByIdsAsync(IEnumerable<int> ids);
        Task<RefuelViewModel> GetByIdAsync(int id);
        Task<IList<RefuelViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0);
        Task<long> RefuelCountAsync();
    }
}
