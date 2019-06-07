using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPAssessmentViewData : NeptuneViewData
    {
        public TreatmentBMPAssessmentViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            
        }
    }
}