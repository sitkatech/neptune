﻿using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentDetailViewData
    {
        public FieldVisitAssessmentType FieldVisitAssessmentType { get; }
        public Models.TreatmentBMPAssessment TreatmentBMPAssessment { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanEdit { get; }
        public ScoreDetailViewData ScoreDetailViewData { get; }
        public string EditBenchmarkAndThresholdUrl { get; }
        public ImageCarouselViewData ImageCarouselViewData { get; }

        public AssessmentDetailViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            FieldVisitAssessmentType = fieldVisitAssessmentType;
            if (treatmentBMPAssessment != null)
            {
                TreatmentBMPAssessment = treatmentBMPAssessment;
                CurrentPersonCanManage =
                    currentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMPAssessment.TreatmentBMP
                        .StormwaterJurisdiction);
                ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment);
                EditBenchmarkAndThresholdUrl =
                    SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x =>
                        x.Instructions(treatmentBMPAssessment.TreatmentBMP));

                CanEdit = CurrentPersonCanManage && treatmentBMPAssessment.CanEdit(currentPerson) &&
                          !treatmentBMPAssessment.IsAssessmentComplete();

                var carouselImages = TreatmentBMPAssessment.TreatmentBMPAssessmentPhotos;
                ImageCarouselViewData = new ImageCarouselViewData(carouselImages, 400);
            }
        }
    }
}