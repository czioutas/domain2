using System.Collections.Generic;

namespace ElectricityCounter.ViewModels
{
    public class TotalVillageReportViewModel
    {
        public string VillageName { get; set; }
        public double Consumption { get; set; }
    }

    public class TotalVillageReportWrapperViewModel
    {
        public List<TotalVillageReportViewModel> Villages { get; set; }
    }
}