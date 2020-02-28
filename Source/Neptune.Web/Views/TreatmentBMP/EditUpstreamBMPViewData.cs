using System.Collections.Generic;
using System.Web.Mvc;

namespace Neptune.Web.Views.TreatmentBMP
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