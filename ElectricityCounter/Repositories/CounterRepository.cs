using ElectricityCounter.Data;
using ElectricityCounter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ElectricityCounter.Repositories
{
    public class CounterRepository : ICounterRepository
    {
        private readonly DatabaseContext _context;

        public CounterRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(string VillageName)
        {
            await _context.Counter.AddAsync(new CounterModel
            {
                VillageName = VillageName
            });

            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public async Task<CounterModel> GetAsync(int id)
        {
            return await _context.Counter.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}