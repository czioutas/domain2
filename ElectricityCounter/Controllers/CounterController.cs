using ElectricityCounter.Models;
using ElectricityCounter.Services;
using ElectricityCounter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectricityCounter.Controllers
{
    [Route("api/v1/[action]")]
    public class CounterController : Controller
    {
        private readonly CounterService _counterService;

        public CounterController(CounterService counterService)
        {
            _counterService = counterService;
        }

        // POST api/vX/CounterCallback
        [HttpPost]
        public async Task<IActionResult> CounterCallback(int counter_id, double amount)
        {
            if (counter_id < 1 || amount < 0)
            {
                return BadRequest("Please provide fully qualified input.");
            }

            bool result = await _counterService.AddConsumptionAsync(new ConsumptionModel { CounterId = counter_id, Amount = amount });

            if (result)
            {
                return Ok();
            }

            return BadRequest("Error");
        }

        // GET api/vX/Counter?id=5
        [HttpGet()]
        public async Task<IActionResult> Counter([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide fully qualified input.");
            }

            SlimCounterViewModel _scm = await _counterService.GetCounterInfoAsync(id);

            if (_scm == null)
            {
                return Ok($"Nothing found for id: {id}");
            }

            return Ok(_scm);
        }

        // GET api/vX/ConsumptionReport?duration=5
        [HttpGet()]
        public async Task<IActionResult> ConsumptionReport([FromQuery] string duration = "24h")
        {
            return Ok(
                new TotalVillageReportWrapperViewModel { Villages = await _counterService.GetTotalReportAsync(duration) }
            );
        }
    }
}