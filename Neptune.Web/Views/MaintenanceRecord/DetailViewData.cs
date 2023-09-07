using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class DetailViewData : NeptuneViewData
    {
        public IOrderedEnumerable<MaintenanceRecordObservation> SortedMaintenanceRecordObservations { get; }
        public string EditUrl { get; }
        public EFModels.Entities.MaintenanceRecord MaintenanceRecord { get; }
        public bool CurrentPersonCanManage { get; }
        public bool BMPTypeHasObservationTypes { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public UrlTemplate<int> CustomAttributeDetailUrlTemplate { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.MaintenanceRecord maintenanceRecord) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = maintenanceRecord.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(maintenanceRecord.TreatmentBMPID));
            PageTitle = maintenanceRecord.GetMaintenanceRecordDate().ToStringDate();
            EditUrl = maintenanceRecord.FieldVisit != null ? SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditMaintenanceRecord(maintenanceRecord.FieldVisit)) : null;
            MaintenanceRecord = maintenanceRecord;
            CurrentPersonCanManage = new MaintenanceRecordManageFeature().HasPermissionByPerson(currentPerson);
            BMPTypeHasObservationTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            CustomAttributeDetailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int))); //todo: change to CustomAttributeController

            SortedMaintenanceRecordObservations = MaintenanceRecord.MaintenanceRecordObservations.ToList()
                .OrderBy(x => x.TreatmentBMPTypeCustomAttributeType.SortOrder)
                .ThenBy(x => x.TreatmentBMPTypeCustomAttributeType.GetDisplayName());
        }
    }
}
