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
        public string SectionName { get; }
        public string SubsectionName { get; set; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string SectionHeader { get; set; }
        public List<string> ValidationWarnings { get; set; }

        public string WrapupUrl { get; }
        public string EditDateAndTypeUrl { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public EFModels.Entities.MaintenanceRecord MaintenanceRecord { get; }
        public EFModels.Entities.TreatmentBMPAssessment InitialAssessment { get; }
        public EFModels.Entities.TreatmentBMPAssessment PostMaintenanceAssessment { get; }
        public string? TreatmentBMPDetailUrl { get; }
        public string MaintenanceRecordDeleteUrl { get; }
        public string InitialAssessmentDeleteUrl { get; }
        public string PostMaintenanceAssessmentDeleteUrl { get; }


        public FieldVisitSectionViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, EFModels.Entities.FieldVisitSection fieldVisitSection)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            FieldVisit = fieldVisit;
            SectionName = fieldVisitSection.FieldVisitSectionName;

            var treatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, c => c.Detail(fieldVisit.TreatmentBMPID));

            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = fieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = fieldVisit.TreatmentBMP.TreatmentBMPName != null ? treatmentBMPDetailUrl : "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();
            TreatmentBMPDetailUrl = treatmentBMPDetailUrl;

            EditDateAndTypeUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditDateAndType(fieldVisit.PrimaryKey));

            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdictionID);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VerifyFieldVisit(FieldVisit.PrimaryKey));

            SectionHeader = fieldVisitSection.SectionHeader;
            ValidationWarnings = new List<string>();

            WrapupUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VisitSummary(fieldVisit));


            MaintenanceRecord = fieldVisit.MaintenanceRecord;
            InitialAssessment = fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial);
            PostMaintenanceAssessment = fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            MaintenanceRecordDeleteUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, c => c.Delete(fieldVisit.MaintenanceRecord));
            InitialAssessmentDeleteUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, c => c.Delete(InitialAssessment));
            PostMaintenanceAssessmentDeleteUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(linkGenerator, c => c.Delete(PostMaintenanceAssessment));


            UserCanDeleteMaintenanceRecord = MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(CurrentPerson, MaintenanceRecord)
                                                 .HasPermission;
        }

        public bool UserCanDeleteAssessment(EFModels.Entities.TreatmentBMPAssessment assessment)
        {
            return assessment != null &&
                   new TreatmentBMPAssessmentManageFeature().HasPermission(CurrentPerson, assessment).HasPermission;
        }
    }
}
