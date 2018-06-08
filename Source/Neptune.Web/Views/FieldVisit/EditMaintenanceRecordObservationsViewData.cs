using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditMaintenanceRecordObservationsViewData : EditAttributesViewData
    {
        public EditMaintenanceRecordObservationsViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose, Models.MaintenanceRecord maintenanceRecord,
            bool isSubForm, bool missingRequiredAttributes) : base(currentPerson, treatmentBMP, customAttributeTypePurpose, isSubForm, missingRequiredAttributes)
        {
            PageTitle = $"Edit Maintenance Record Observations";
            ParentDetailUrl = maintenanceRecord.GetDetailUrl();
        }
    }
}