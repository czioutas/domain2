using ElectricityCounter.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricityCounter.Data.Seed
{
    public class ProductionSeed
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
            List<CounterModel> c = new List<CounterModel>();
            c.Add(new CounterModel { VillageName = "Villarriba" });
            c.Add(new CounterModel { VillageName = "Villarrijo" });

            await context.Counter.AddRangeAsync(c);
            await context.SaveChangesAsync();
        }
    }
}