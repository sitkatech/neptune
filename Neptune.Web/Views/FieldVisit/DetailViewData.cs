using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FieldVisit
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.FieldVisit FieldVisit { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public IOrderedEnumerable<MaintenanceRecordObservation> SortedMaintenanceRecordObservations { get; }
        public AssessmentDetailViewData InitialAssessmentViewData { get; }
        public AssessmentDetailViewData PostMaintenanceAssessmentViewData { get; }
        public EFModels.Entities.MaintenanceRecord MaintenanceRecord { get; }
        public EFModels.Entities.TreatmentBMPAssessment InitialAssessment { get; }
        public EFModels.Entities.TreatmentBMPAssessment PostMaintenanceAssessment { get; }
        public bool UserCanDeleteInitialAssessment { get; }
        public bool UserCanDeletePostMaintenanceAssessment { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string MarkAsProvisionalUrl { get; }
        public string ReturnToEditUrl { get; }
        public bool CanEditStormwaterJurisdiction { get; }
        public string TreatmentBMPDetailUrl { get; }
        public UrlTemplate<int> CustomAttributeDetailUrlTemplate { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit,
            AssessmentDetailViewData initialAssessmentViewData,
            AssessmentDetailViewData postMaintenanceAssessmentViewData) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            FieldVisit = fieldVisit;
            MaintenanceRecord = fieldVisit.MaintenanceRecord;
            InitialAssessmentViewData = initialAssessmentViewData;
            PostMaintenanceAssessmentViewData = postMaintenanceAssessmentViewData;
            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = fieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = fieldVisit.TreatmentBMP.TreatmentBMPName != null ? SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(fieldVisit.TreatmentBMPID)) : "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();
            InitialAssessment = FieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial);
            UserCanDeleteInitialAssessment = InitialAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, InitialAssessment.TreatmentBMP)
                                                 .HasPermission;
            PostMaintenanceAssessment = FieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            UserCanDeletePostMaintenanceAssessment = PostMaintenanceAssessment != null &&
                                                     new TreatmentBMPAssessmentManageFeature()
                                                         .HasPermission(currentPerson, PostMaintenanceAssessment.TreatmentBMP)
                                                         .HasPermission;
            UserCanDeleteMaintenanceRecord = MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(currentPerson, MaintenanceRecord.TreatmentBMP)
                                                 .HasPermission;
            SortedMaintenanceRecordObservations = MaintenanceRecord?.MaintenanceRecordObservations.ToList()
                .OrderBy(x => x.TreatmentBMPTypeCustomAttributeType.SortOrder)
                .ThenBy(x => x.TreatmentBMPTypeCustomAttributeType.GetDisplayName());
            UserHasCustomAttributeTypeManagePermissions =
                new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanManageStormwaterJurisdiction =
                currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdictionID);
            CanEditStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdictionID);
            VerifiedUnverifiedFieldVisitUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VerifyFieldVisit(fieldVisit.PrimaryKey));
            MarkAsProvisionalUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.MarkProvisionalFieldVisit(fieldVisit.PrimaryKey));
            ReturnToEditUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.ReturnFieldVisitToEdit(fieldVisit.PrimaryKey));
            TreatmentBMPDetailUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(fieldVisit.TreatmentBMPID));
            CustomAttributeDetailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int))); //todo: change to CustomAttributeController
        }
    }
}
