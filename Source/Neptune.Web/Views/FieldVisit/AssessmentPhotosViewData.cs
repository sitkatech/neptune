using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentPhotosViewData : FieldVisitSectionViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; set; }

        public AssessmentPhotosViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment,
            Models.FieldVisitSection fieldVisitSection, ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(currentPerson, treatmentBMPAssessment.GetFieldVisit(), fieldVisitSection)
        {
            SubsectionName = "Photos";
            SectionHeader = $"{SectionHeader} - {SubsectionName}";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
