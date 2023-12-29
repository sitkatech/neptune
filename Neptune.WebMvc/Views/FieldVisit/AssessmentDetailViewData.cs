using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.TreatmentBMPAssessment;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class AssessmentDetailViewData : NeptuneUserControlViewData
    {
        public TreatmentBMPAssessmentTypeEnum TreatmentBMPAssessmentTypeEnum { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public EFModels.Entities.TreatmentBMPAssessment? TreatmentBMPAssessment { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanEdit { get; }
        public ScoreDetailViewData ScoreDetailViewData { get; }
        public string EditBenchmarkAndThresholdUrl { get; }
        public ImageCarouselViewData ImageCarouselViewData { get; }

        public AssessmentDetailViewData(LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMPAssessment? treatmentBMPAssessment, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos)
        {
            TreatmentBMPAssessmentTypeEnum = treatmentBMPAssessmentTypeEnum;
            TreatmentBMPType = treatmentBMPType;
            if (treatmentBMPAssessment != null)
            {
                TreatmentBMPAssessment = treatmentBMPAssessment;
                CurrentPersonCanManage = currentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdictionID);
                ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment, treatmentBMPType);
                EditBenchmarkAndThresholdUrl =
                    SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.Instructions(treatmentBMPAssessment.TreatmentBMP));

                CanEdit = CurrentPersonCanManage && treatmentBMPAssessment.CanEdit(currentPerson) &&
                          !treatmentBMPAssessment.IsAssessmentComplete;

                ImageCarouselViewData = new ImageCarouselViewData(treatmentBMPAssessmentPhotos, 400, linkGenerator);
            }
        }
    }
}