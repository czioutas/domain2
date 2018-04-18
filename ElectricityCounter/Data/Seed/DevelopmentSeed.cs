using ElectricityCounter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricityCounter.Data.Seed
{
    public static class DevelopmentSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using (DatabaseContext context = services.GetRequiredService<DatabaseContext>())
            {
                await AddVillageCounters(context);
            }
        }

        private static async Task AddVillageCounters(DatabaseContext context)
        {
            if (await context.Counter.CountAsync() != 0)
            {
                return;
            }

            List<CounterModel> c = new List<CounterModel>();
            c.Add(new CounterModel { VillageName = "Villarriba" });
            c.Add(new CounterModel { VillageName = "Villarrijo" });

            await context.Counter.AddRangeAsync(c);
            await context.SaveChangesAsync();
        }
    }
}