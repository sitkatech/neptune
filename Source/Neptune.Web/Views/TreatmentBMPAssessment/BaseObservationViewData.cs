using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public abstract class BaseObservationViewData : NeptuneViewData
    {
        // ReSharper disable InconsistentNaming
        public readonly Models.TreatmentBMPAssessment TreatmentBMPAssessment;
        public readonly Models.TreatmentBMP TreatmentBMP;
        public readonly string AssessmentInformationUrl;
        public readonly string ScoreUrl;
        public readonly string SectionName;
        public readonly bool AssessmentInformationComplete;
        public readonly bool DisableInputs;

        protected BaseObservationViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, string sectionName, bool disableInputs)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            TreatmentBMP = treatmentBMPAssessment.TreatmentBMP;
            TreatmentBMPAssessment = treatmentBMPAssessment;

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {
                AssessmentInformationUrl = "#";
                AssessmentInformationComplete = false;
            }
            else
            {
                AssessmentInformationUrl =
                    SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                        x.Edit(TreatmentBMPAssessment));
                AssessmentInformationComplete = true;
            }

            ScoreUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                x.Score(TreatmentBMPAssessment));

            SectionName = sectionName;

            DisableInputs = disableInputs;

            EntityName = "Treatment BMP Assessments";
            EntityUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = treatmentBMPAssessment.TreatmentBMP?.FormattedNameAndType ?? "Preview Treatment BMP Assessment";
            SubEntityUrl = treatmentBMPAssessment.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = treatmentBMPAssessment.AssessmentDate.ToStringDate();
        }
    }
}
