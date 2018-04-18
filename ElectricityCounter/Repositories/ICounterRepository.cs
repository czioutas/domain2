using ElectricityCounter.Models;
using System.Threading.Tasks;

namespace ElectricityCounter.Repositories
{
    public interface ICounterRepository
    {
        Task<CounterModel> GetAsync(int id);

        Task<bool> AddAsync(string VillageName);
    }
}