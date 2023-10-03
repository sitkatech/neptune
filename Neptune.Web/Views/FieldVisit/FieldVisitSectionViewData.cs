using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitSectionViewData : NeptuneViewData
    {
        public EFModels.Entities.FieldVisit FieldVisit { get; }
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public string SectionName { get; }
        public string SubsectionName { get; set; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string SectionHeader { get; set; }
        public List<string> ValidationWarnings { get; set; }

        public string WrapupUrl { get; }
        public string EditDateAndTypeUrl { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public EFModels.Entities.MaintenanceRecord? MaintenanceRecord { get; }
        public EFModels.Entities.TreatmentBMPAssessment? InitialAssessment { get; }
        public EFModels.Entities.TreatmentBMPAssessment? PostMaintenanceAssessment { get; }
        public string? TreatmentBMPDetailUrl { get; }
        public string MaintenanceRecordDeleteUrl { get; }
        public string InitialAssessmentDeleteUrl { get; }
        public string PostMaintenanceAssessmentDeleteUrl { get; }


        public FieldVisitSectionViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit, EFModels.Entities.FieldVisitSection fieldVisitSection,
            EFModels.Entities.TreatmentBMPType treatmentBMPType,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            FieldVisit = fieldVisit;
            TreatmentBMPType = treatmentBMPType;
            var treatmentBMP = fieldVisit.TreatmentBMP;
            TreatmentBMP = treatmentBMP;
            SectionName = fieldVisitSection.FieldVisitSectionName;

            var treatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(fieldVisit.TreatmentBMPID));

            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = treatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = treatmentBMP.TreatmentBMPName != null ? treatmentBMPDetailUrl : "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();
            TreatmentBMPDetailUrl = treatmentBMPDetailUrl;

            EditDateAndTypeUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditDateAndType(fieldVisit.FieldVisitID));

            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VerifyFieldVisit(fieldVisit.FieldVisitID));

            SectionHeader = fieldVisitSection.SectionHeader;
            ValidationWarnings = new List<string>();

            WrapupUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VisitSummary(fieldVisit));

            MaintenanceRecord = maintenanceRecord;
            MaintenanceRecordDeleteUrl = maintenanceRecord == null ? string.Empty : SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(maintenanceRecord));
            UserCanDeleteMaintenanceRecord = maintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(CurrentPerson, treatmentBMP)
                                                 .HasPermission;

            var initialAssessment = treatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentTypeID == (int) TreatmentBMPAssessmentTypeEnum.Initial);
            InitialAssessment = initialAssessment;
            InitialAssessmentDeleteUrl = initialAssessment == null ? string.Empty : SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(initialAssessment));

            var postMaintenanceAssessment = treatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentTypeID == (int)TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            PostMaintenanceAssessment = postMaintenanceAssessment;
            PostMaintenanceAssessmentDeleteUrl = postMaintenanceAssessment == null ? string.Empty : SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(postMaintenanceAssessment));
        }

        public bool UserCanDeleteAssessment(EFModels.Entities.TreatmentBMPAssessment? assessment)
        {
            return assessment != null &&
                   new TreatmentBMPAssessmentManageFeature().HasPermission(CurrentPerson, assessment.TreatmentBMP).HasPermission;
        }
    }
}
