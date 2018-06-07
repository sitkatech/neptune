using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentPhotosViewData : FieldVisitSectionViewData
    {
        public IDictionary<int, TreatmentBMPAssessmentPhoto> PhotosByID { get; set; }

        public AssessmentPhotosViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment,
            Models.FieldVisitSection fieldVisitSection)
            : base(currentPerson, treatmentBMPAssessment.GetFieldVisit(), fieldVisitSection)
        {
            SubsectionName = "Photos";
            SectionHeader = $"{SectionHeader} - {SubsectionName}";
            PhotosByID = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.ToDictionary(x => x.TreatmentBMPAssessmentPhotoID, x => x);
        }
    }
}
