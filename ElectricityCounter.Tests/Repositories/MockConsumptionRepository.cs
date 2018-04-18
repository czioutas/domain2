using ElectricityCounter.Models;
using ElectricityCounter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenFu;
using System.Threading.Tasks;

namespace ElectricityCounter.Tests.Repositories
{
    internal class MockConsumptionRepository : IConsumptionRepository
    {
        private List<ConsumptionModel> _consumptionList = new List<ConsumptionModel>();

        public Task<bool> AddAsync(ConsumptionModel model)
        {
            model.Id = _consumptionList.Count() + 1;
            _consumptionList.Add(model);

            return Task.FromResult(true);
        }

        public Task<List<ConsumptionModel>> GetAsync(int hours)
        {
            DateTime past = DateTime.Now.AddHours(-hours);

            var list = _consumptionList.Where(o => o.CreatedAt >= past).ToList();

            list.ForEach(i => i.Counter = A.New<CounterModel>());

            return Task.FromResult(list);
        }
    }
}