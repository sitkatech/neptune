using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.FieldVisit FieldVisit { get; }
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public AssessmentDetailViewData InitialAssessmentViewData { get; }
        public AssessmentDetailViewData PostMaintenanceAssessmentViewData { get; }
        public EFModels.Entities.MaintenanceRecord? MaintenanceRecord { get; }
        public EFModels.Entities.TreatmentBMPAssessment? InitialAssessment { get; }
        public EFModels.Entities.TreatmentBMPAssessment? PostMaintenanceAssessment { get; }
        public bool UserCanDeleteInitialAssessment { get; }
        public bool UserCanDeletePostMaintenanceAssessment { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string MarkAsProvisionalUrl { get; }
        public string ReturnToEditUrl { get; }
        public bool CanEditStormwaterJurisdiction { get; }
        public string TreatmentBMPDetailUrl { get; }
        public UrlTemplate<int> CustomAttributeTypeDetailUrlTemplate { get; }
        public List<CustomAttribute> TreatmentBMPCustomAttributes { get; }
        public List<TreatmentBMPTypeCustomAttributeType> ObservationTypes { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit,
            AssessmentDetailViewData initialAssessmentViewData,
            AssessmentDetailViewData postMaintenanceAssessmentViewData, List<CustomAttribute> treatmentBMPCustomAttributes, EFModels.Entities.TreatmentBMPType treatmentBMPType, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            FieldVisit = fieldVisit;
            MaintenanceRecord = maintenanceRecord;
            InitialAssessmentViewData = initialAssessmentViewData;
            PostMaintenanceAssessmentViewData = postMaintenanceAssessmentViewData;
            TreatmentBMPCustomAttributes = treatmentBMPCustomAttributes;
            TreatmentBMPType = treatmentBMPType;
            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            var treatmentBMP = fieldVisit.TreatmentBMP;
            TreatmentBMP = treatmentBMP;
            SubEntityName = treatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = treatmentBMP.TreatmentBMPName != null ? SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP.TreatmentBMPID)) : "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();
            InitialAssessment = initialAssessmentViewData.TreatmentBMPAssessment;
            UserCanDeleteInitialAssessment = initialAssessmentViewData.TreatmentBMPAssessment != null &&
                                             new TreatmentBMPAssessmentManageFeature()
                                                 .HasPermission(currentPerson, treatmentBMP)
                                                 .HasPermission;
            PostMaintenanceAssessment = postMaintenanceAssessmentViewData.TreatmentBMPAssessment;
            UserCanDeletePostMaintenanceAssessment = postMaintenanceAssessmentViewData.TreatmentBMPAssessment != null &&
                                                     new TreatmentBMPAssessmentManageFeature()
                                                         .HasPermission(currentPerson, treatmentBMP)
                                                         .HasPermission;
            UserCanDeleteMaintenanceRecord = MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(currentPerson, treatmentBMP)
                                                 .HasPermission;
            UserHasCustomAttributeTypeManagePermissions =
                new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanManageStormwaterJurisdiction =
                currentPerson.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            CanEditStormwaterJurisdiction = currentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            VerifiedUnverifiedFieldVisitUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VerifyFieldVisit(fieldVisit.PrimaryKey));
            MarkAsProvisionalUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.MarkProvisionalFieldVisit(fieldVisit.PrimaryKey));
            ReturnToEditUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.ReturnFieldVisitToEdit(fieldVisit.PrimaryKey));
            TreatmentBMPDetailUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP.TreatmentBMPID));
            CustomAttributeTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            ObservationTypes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).OrderBy(x => x.SortOrder).ThenBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
        }
    }
}
