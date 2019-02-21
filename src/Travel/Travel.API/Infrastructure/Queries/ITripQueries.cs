using System.Threading.Tasks;
using Travel.API.ViewModel;

namespace Travel.API.Infrastructure.Queries
{
    public interface ITripQueries
    {
        Task<TripViewModel> GetTripByIdAsync(int id);

    }
}