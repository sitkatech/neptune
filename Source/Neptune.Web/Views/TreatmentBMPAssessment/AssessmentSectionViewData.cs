using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class AssessmentSectionViewData : NeptuneViewData
    {
        public Models.TreatmentBMPAssessment TreatmentBMPAssessment { get; }
        public string AssessmentInformationUrl { get; }
        public string ScoreUrl { get; }
        public string SectionName { get; }
        public bool AssessmentInformationComplete { get; }

        public AssessmentSectionViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, string sectionName)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            TreatmentBMPAssessment = treatmentBMPAssessment;

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {
                AssessmentInformationUrl = "#";
                AssessmentInformationComplete = false;
            }
            else
            {
                //AssessmentInformationUrl =
                //    SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                //        x.Edit(TreatmentBMPAssessment));
                //AssessmentInformationComplete = true;
            }

            ScoreUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                x.Score(TreatmentBMPAssessment));

            SectionName = sectionName;

            EntityName = "Treatment BMP Assessments";
            EntityUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = treatmentBMPAssessment.TreatmentBMP?.FormattedNameAndType ?? "Preview Treatment BMP Assessment";
            SubEntityUrl = treatmentBMPAssessment.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = treatmentBMPAssessment.AssessmentDate.ToStringDate();
        }
    }
}
