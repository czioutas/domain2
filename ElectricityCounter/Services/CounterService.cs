using ElectricityCounter.Models;
using ElectricityCounter.Repositories;
using ElectricityCounter.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityCounter.Services
{
    public class CounterService
    {
        private readonly ICounterRepository _counterRepository;
        private readonly IConsumptionRepository _consumptionRepository;

        public CounterService(ICounterRepository counterRepository, IConsumptionRepository consumptionRepository)
        {
            _counterRepository = counterRepository;
            _consumptionRepository = consumptionRepository;
        }

        public async Task<SlimCounterViewModel> GetCounterInfoAsync(int id)
        {
            CounterModel _cm = await _counterRepository.GetAsync(id);

            if (_cm == null)
            {
                return null;
            }

            return new SlimCounterViewModel { Id = _cm.Id.ToString(), VillageName = _cm.VillageName };
        }

        public async Task<bool> AddConsumptionAsync(ConsumptionModel consumption)
        {
            return await _consumptionRepository.AddAsync(consumption);
        }

        public async Task<List<TotalVillageReportViewModel>> GetTotalReportAsync(string duration)
        {
            int hours = DurationToHours(duration);

            IList<ConsumptionModel> consumption = await _consumptionRepository.GetAsync(hours);

            return consumption
                .GroupBy(o => o.CounterId)
                .Select(g => new TotalVillageReportViewModel
                {
                    VillageName = g.FirstOrDefault().Counter.VillageName,
                    Consumption = g.Sum(s => s.Amount)
                })
                .ToList();
        }

        private int DurationToHours(string duration)
        {
            if (duration.Contains("h"))
            {
                return int.Parse(duration.Replace("h", ""));
            }
            else if (duration.Contains("d"))
            {
                return int.Parse(duration.Replace("h", "")) * 24;
            }
            else if (duration.Contains("w"))
            {
                return int.Parse(duration.Replace("w", "")) * 24 * 7;
            }

            if (int.TryParse(duration, out int result))
            {
                return result;
            }

            return 24;
        }
    }
}