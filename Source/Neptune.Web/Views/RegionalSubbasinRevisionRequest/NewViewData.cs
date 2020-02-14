using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }

        public NewViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            EntityName = "Regional Subbasin";
            PageTitle = "Revision";
        }
    }
}