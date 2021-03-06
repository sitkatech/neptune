﻿using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTreatmentBMPSimple : IAuditableEntity
    {
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public int TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }
        public string TreatmentBMPType { get; set; }


        public string FieldVisiLastVisitedtDate { get; set; }
        public string FieldVisitMostRecentScore { get; set; }
        public string TreatmentBMPDetailUrl { get; set; }

        public WaterQualityManagementPlanVerifyTreatmentBMPSimple()
        {
        }
        public WaterQualityManagementPlanVerifyTreatmentBMPSimple(Models.TreatmentBMP treatmentBMP)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = null;
            TreatmentBMPName = treatmentBMP.TreatmentBMPName;
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            IsAdequate = null;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = null;
            TreatmentBMPType = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName;

            var mostRecentFieldVisit = treatmentBMP.FieldVisits.Where(x => x.FieldVisitStatus == FieldVisitStatus.Complete).OrderBy(x => x.VisitDate).FirstOrDefault();
            FieldVisiLastVisitedtDate = mostRecentFieldVisit?.VisitDate.ToShortDateString();
            FieldVisitMostRecentScore = mostRecentFieldVisit?.GetPostMaintenanceAssessment() != null ? mostRecentFieldVisit.GetPostMaintenanceAssessment().FormattedScore() :  mostRecentFieldVisit?.GetInitialAssessment().FormattedScore();
            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c =>
                c.Detail(treatmentBMP.PrimaryKey));
        }

        public WaterQualityManagementPlanVerifyTreatmentBMPSimple(Models.WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP
                .WaterQualityManagementPlanVerifyTreatmentBMPID;
            TreatmentBMPName = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.TreatmentBMPName;
            TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMPID;
            TreatmentBMPType = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName;
            IsAdequate = waterQualityManagementPlanVerifyTreatmentBMP.IsAdequate;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPNote;

            var mostRecentFieldVisit = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.FieldVisits.Where(x => x.FieldVisitStatus == FieldVisitStatus.Complete).OrderBy(x => x.VisitDate).FirstOrDefault();
            FieldVisiLastVisitedtDate = mostRecentFieldVisit?.VisitDate.ToShortDateString();
            FieldVisitMostRecentScore = mostRecentFieldVisit?.GetPostMaintenanceAssessment() != null ? mostRecentFieldVisit.GetPostMaintenanceAssessment().FormattedScore() : mostRecentFieldVisit?.GetInitialAssessment().FormattedScore();
            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c =>
                c.Detail(waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.PrimaryKey));
        }

        
        public string GetAuditDescriptionString()
        {
            return TreatmentBMPName;
        }
    }
}