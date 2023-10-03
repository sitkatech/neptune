using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class AssessmentSectionViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMPAssessment TreatmentBMPAssessment { get; }
        public string AssessmentInformationUrl { get; }
        public string ScoreUrl { get; }
        public string SectionName { get; }
        public bool AssessmentInformationComplete { get; }

        public AssessmentSectionViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment, string sectionName)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBMPAssessment = treatmentBMPAssessment;

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {
                AssessmentInformationUrl = "#";
                AssessmentInformationComplete = false;
            }

            ScoreUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x =>
                x.Score(TreatmentBMPAssessment));

            SectionName = sectionName;

            EntityName = "Treatment BMP Assessments";
            EntityUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Assessment";
            SubEntityUrl = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPName != null ? SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMPAssessment.TreatmentBMPID)) :  "#";
            PageTitle = treatmentBMPAssessment.GetAssessmentDate().ToStringDate();
        }
    }
}
