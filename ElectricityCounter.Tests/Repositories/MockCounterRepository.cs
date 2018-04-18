using ElectricityCounter.Models;
using ElectricityCounter.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using GenFu;
using System.Threading.Tasks;
using System.Linq;

namespace ElectricityCounter.Tests.Repositories
{
    public class MockCounterRepository : ICounterRepository
    {
        private List<CounterModel> _counters = new List<CounterModel>{
            new CounterModel { Id = 1, VillageName = "TestVillage1" },
            new CounterModel { Id = 2, VillageName = "TestVillage2" }
        };

        public Task<bool> AddAsync(string VillageName)
        {
            _counters.Add(new CounterModel { Id = _counters.Count() + 1, VillageName = VillageName });
            return Task.FromResult(true);
        }

        public Task<CounterModel> GetAsync(int id)
        {
            return Task.FromResult(_counters.FirstOrDefault(o => o.Id == id));
        }
    }
}