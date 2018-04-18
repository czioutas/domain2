using System.ComponentModel.DataAnnotations;

namespace ElectricityCounter.ViewModels
{
    public class CounterConsumptionPostViewModel
    {
        [Required]
        public string CounterId { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}