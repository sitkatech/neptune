using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ModeledBMPPerformanceViewData: NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }

        public ModeledBMPPerformanceViewData(Models.TreatmentBMP treatmentBMP, Models.Person person): base(person, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
        }
    }
}