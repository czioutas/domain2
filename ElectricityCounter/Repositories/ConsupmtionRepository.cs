using ElectricityCounter.Data;
using ElectricityCounter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityCounter.Repositories
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly DatabaseContext _context;

        public ConsumptionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(ConsumptionModel model)
        {
            await _context.Consumption.AddAsync(model);
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public async Task<List<ConsumptionModel>> GetAsync(int hours)
        {
            DateTime past = DateTime.Now.AddHours(-hours);

            return await _context.Consumption.AsNoTracking().Where(o => o.CreatedAt >= past).Include(o => o.Counter).ToListAsync();
        }
    }
}