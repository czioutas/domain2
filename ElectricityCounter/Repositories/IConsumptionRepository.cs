using ElectricityCounter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricityCounter.Repositories
{
    public interface IConsumptionRepository
    {
        Task<bool> AddAsync(ConsumptionModel model);

        Task<List<ConsumptionModel>> GetAsync(int hours);
    }
}