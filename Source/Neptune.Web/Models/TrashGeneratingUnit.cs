using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TrashGeneratingUnit
    {
        public OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea =>
            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas
                .Find(OnlandVisualTrashAssessmentAreaID);
        public TreatmentBMP TreatmentBMP => DelineationID != null ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Single(x=>x.Delineation.DelineationID == DelineationID) : null;

        public Delineation Delineation =>
            DelineationID != null ? HttpRequestStorage.DatabaseEntities.Delineations.Find(DelineationID) : null;
        public WaterQualityManagementPlan WaterQualityManagementPlan => WaterQualityManagementPlanID != null ? HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Find(WaterQualityManagementPlanID) : null;

        public bool IsFullTrashCapture => (this.TreatmentBMP?.TrashCaptureStatusTypeID ==
                                           TrashCaptureStatusType.Full.TrashCaptureStatusTypeID ||
                                           this.WaterQualityManagementPlan?.TrashCaptureStatusTypeID ==
                                           TrashCaptureStatusType.Full.TrashCaptureStatusTypeID);
    }
}