using ElectricityCounter.Controllers;
using ElectricityCounter.Repositories;
using ElectricityCounter.Services;
using ElectricityCounter.Tests.Repositories;
using ElectricityCounter.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ElectricityCounter.Tests
{
    public class CounterControllerTest
    {
        private IConsumptionRepository _consumptionRepository;
        private ICounterRepository _counterRepository;
        private CounterService _counterService;
        private CounterController _controller;

        // This test ensures that it will retrieve the correct counter
        [Fact]
        public async Task Counter_Get_One()
        {
            _consumptionRepository = new MockConsumptionRepository();
            _counterRepository = new MockCounterRepository();
            _counterService = new CounterService(_counterRepository, _consumptionRepository);
            _controller = new CounterController(_counterService);

            var result = await _controller.Counter(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var subject = okResult.Value.Should().BeAssignableTo<SlimCounterViewModel>().Subject;
            subject.Id.Should().Be("1");
            subject.VillageName.Should().Be("TestVillage1");
        }

        // This test ensures that the post returns a 200
        [Fact]
        public async Task Consumption_Post_Once()
        {
            _consumptionRepository = new MockConsumptionRepository();
            _counterRepository = new MockCounterRepository();
            _counterService = new CounterService(_counterRepository, _consumptionRepository);
            _controller = new CounterController(_counterService);

            var result = await _controller.CounterCallback(1, 244);

            var okResult = result.Should().BeOfType<OkResult>().Subject;
        }

        // This test ensures the report provides correct summing of the data
        [Fact]
        public async Task Counter_Report_All()
        {
            _consumptionRepository = new MockConsumptionRepository();
            _counterRepository = new MockCounterRepository();
            _counterService = new CounterService(_counterRepository, _consumptionRepository);
            _controller = new CounterController(_counterService);

            await _controller.CounterCallback(1, 250);
            await _controller.CounterCallback(1, 240);
            await _controller.CounterCallback(2, 2444);

            var result = await _controller.ConsumptionReport("24");

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            var subject = okResult.Value.Should().BeAssignableTo<TotalVillageReportWrapperViewModel>().Subject;

            subject.Villages.Count().Should().Be(2);

            subject.Villages[0].Consumption.Should().Be(490);
            subject.Villages[1].Consumption.Should().Be(2444);
        }
    }
}