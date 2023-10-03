using System.Collections.Generic;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class FindABMPViewModel
    {
        public string SearchTerm { get; set; }
        public List<int> TreatmentBMPTypeIDs { get; set; }
        public List<int> StormwaterJurisdictionIDs { get; set; }
    }
}