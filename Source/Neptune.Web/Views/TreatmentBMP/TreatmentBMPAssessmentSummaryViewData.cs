using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPAssessmentSummaryViewData : NeptuneViewData
    {
        public TreatmentBMPAssessmentSummaryViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            
        }
    }
}