using System.Collections;
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views;

namespace Neptune.Web.Views.FieldVisit
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.FieldVisit FieldVisit { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get;  }
        public IOrderedEnumerable<MaintenanceRecordObservation> SortedMaintenanceRecordObservations { get; }

        public AssessmentDetailViewData InitialAssessmentViewData { get; }
        public AssessmentDetailViewData PostMaintenanceAssessmentViewData { get; }
        public Models.MaintenanceRecord MaintenanceRecord { get; }
        public bool UserCanDeleteInitialAssessment { get; }
        public bool UserCanDeletePostMaintenanceAssessment { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string MarkAsProvisionalUrl { get; set; }

        public DetailViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.FieldVisit fieldVisit, AssessmentDetailViewData initialAssessmentViewData, AssessmentDetailViewData postMaintenanceAssessmentViewData) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            FieldVisit  = fieldVisit;
            MaintenanceRecord = FieldVisit.MaintenanceRecord;
            InitialAssessmentViewData = initialAssessmentViewData;
            PostMaintenanceAssessmentViewData = postMaintenanceAssessmentViewData;
            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = FieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = FieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = FieldVisit.VisitDate.ToStringDate();
            UserCanDeleteInitialAssessment = FieldVisit.InitialAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, FieldVisit.InitialAssessment)
                                                 .HasPermission;
            UserCanDeletePostMaintenanceAssessment = FieldVisit.PostMaintenanceAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, FieldVisit.PostMaintenanceAssessment)
                                                 .HasPermission;
            UserCanDeleteMaintenanceRecord = FieldVisit.MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(currentPerson, FieldVisit.MaintenanceRecord)
                                                 .HasPermission;
            SortedMaintenanceRecordObservations = FieldVisit.MaintenanceRecord?.MaintenanceRecordObservations.ToList()
                .OrderBy(x => x.TreatmentBMPTypeCustomAttributeType.SortOrder)
                .ThenBy(x => x.TreatmentBMPTypeCustomAttributeType.GetDisplayName());
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            
            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdiction);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.VerifyFieldVisit(FieldVisit.PrimaryKey));
            MarkAsProvisionalUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.MarkProvisionalFieldVisit(FieldVisit.PrimaryKey));
        }

    }
}
