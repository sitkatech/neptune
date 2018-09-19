using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

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
        public Models.TreatmentBMPAssessment InitialAssessment { get; }
        public Models.TreatmentBMPAssessment PostMaintenanceAssessment { get; }
        public bool UserCanDeleteInitialAssessment { get; }
        public bool UserCanDeletePostMaintenanceAssessment { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string MarkAsProvisionalUrl { get; set; }

        public DetailViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.FieldVisit fieldVisit, AssessmentDetailViewData initialAssessmentViewData, AssessmentDetailViewData postMaintenanceAssessmentViewData) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            FieldVisit  = fieldVisit;
            MaintenanceRecord = FieldVisit.GetMaintenanceRecord();
            InitialAssessmentViewData = initialAssessmentViewData;
            PostMaintenanceAssessmentViewData = postMaintenanceAssessmentViewData;
            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = FieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = FieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = FieldVisit.VisitDate.ToStringDate();
            InitialAssessment = FieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial);
            UserCanDeleteInitialAssessment = InitialAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, InitialAssessment)
                                                 .HasPermission;
            PostMaintenanceAssessment = FieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            UserCanDeletePostMaintenanceAssessment = PostMaintenanceAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, PostMaintenanceAssessment)
                                                 .HasPermission;
            UserCanDeleteMaintenanceRecord = MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(currentPerson, MaintenanceRecord)
                                                 .HasPermission;
            SortedMaintenanceRecordObservations = MaintenanceRecord?.MaintenanceRecordObservations.ToList()
                .OrderBy(x => x.TreatmentBMPTypeCustomAttributeType.SortOrder)
                .ThenBy(x => x.TreatmentBMPTypeCustomAttributeType.GetDisplayName());
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            
            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdiction);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.VerifyFieldVisit(FieldVisit.PrimaryKey));
            MarkAsProvisionalUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.MarkProvisionalFieldVisit(FieldVisit.PrimaryKey));
        }

    }
}
