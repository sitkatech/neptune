using Microsoft.AspNetCore.Mvc.Rendering;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditUpstreamBMPViewData : NeptuneUserControlViewData
    {
        public IEnumerable<SelectListItem> TreatmentBMPList { get; }

        public EditUpstreamBMPViewData(IEnumerable<SelectListItem> treatmentBMPSelectList)
        {
            TreatmentBMPList = treatmentBMPSelectList;
        }
    }
}