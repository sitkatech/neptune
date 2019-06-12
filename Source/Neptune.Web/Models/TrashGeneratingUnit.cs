using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TrashGeneratingUnit
    {
        public OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea =>
            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas
                .Find(OnlandVisualTrashAssessmentAreaID);
        public TreatmentBMP TreatmentBMP => Delineation != null ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Find(Delineation.TreatmentBMPID) : null;
    }
}