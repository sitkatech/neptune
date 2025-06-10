using System.Collections.Generic;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class FindAWQMPViewModel
    {
        public string SearchTerm { get; set; }
        //public List<int> WaterQualityManagementPlanIDs { get; set; }
        public List<int> StormwaterJurisdictionIDs { get; set; }
    }
}