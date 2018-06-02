using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class EditMaintenanceRecordObservationsViewData : EditAttributesViewData
    {
        public EditMaintenanceRecordObservationsViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose, Models.MaintenanceRecord maintenanceRecord, bool isSubForm) : base(currentPerson, treatmentBMP, customAttributeTypePurpose, isSubForm)
        {
            PageTitle = $"Edit Maintenance Record Observations";
            ParentDetailUrl = maintenanceRecord.GetDetailUrl();
        }
    }
}