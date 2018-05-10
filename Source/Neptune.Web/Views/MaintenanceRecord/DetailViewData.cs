using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class DetailViewData : NeptuneViewData
    {
        public DetailViewData(Person currentPerson, Models.MaintenanceRecord maintenanceRecord) : base(currentPerson)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = maintenanceRecord.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = maintenanceRecord.TreatmentBMP.GetDetailUrl();
            PageTitle = "Maintenance Record Detail";
            EditUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.Edit(maintenanceRecord));
            EditObservationsUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(x => x.EditObservations(maintenanceRecord));
            MaintenanceRecord = maintenanceRecord;
            CurrentPersonCanManage = new MaintenanceRecordManageFeature().HasPermissionByPerson(currentPerson);
            BMPTypeHasObservationTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x => x.CustomAttributeType.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            UserHasCustomAttributeTypeManagePermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
        }

        public string EditObservationsUrl { get; }

        public string EditUrl { get; }
        public Models.MaintenanceRecord MaintenanceRecord { get; }
        public bool CurrentPersonCanManage { get; }
        public bool BMPTypeHasObservationTypes { get; }
        public bool UserHasCustomAttributeTypeManagePermissions { get; set; }
    }
}