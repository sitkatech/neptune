using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.MaintenanceRecord
{
    public class DetailViewData : NeptuneViewData
    {
        public List<TreatmentBMPTypeCustomAttributeType> ObservationTypes { get; }
        public string EditUrl { get; }
        public EFModels.Entities.MaintenanceRecord MaintenanceRecord { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public bool CurrentPersonCanManage { get; }
        public bool HasObservationTypes { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public UrlTemplate<int> CustomAttributeTypeDetailUrlTemplate { get; }
        public string OrganizationUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.MaintenanceRecord maintenanceRecord, EFModels.Entities.TreatmentBMPType treatmentBMPType) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = maintenanceRecord.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(maintenanceRecord.TreatmentBMPID));
            PageTitle = maintenanceRecord.GetMaintenanceRecordDate().ToStringDate();
            EditUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditMaintenanceRecord(maintenanceRecord.FieldVisit));
            MaintenanceRecord = maintenanceRecord;
            TreatmentBMPType = treatmentBMPType;
            CurrentPersonCanManage = new MaintenanceRecordManageFeature().HasPermissionByPerson(currentPerson);
            ObservationTypes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).OrderBy(x => x.SortOrder).ThenBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
            HasObservationTypes = ObservationTypes.Any();
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            CustomAttributeTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            OrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(maintenanceRecord.FieldVisit.PerformedByPerson.OrganizationID));
        }
    }
}
