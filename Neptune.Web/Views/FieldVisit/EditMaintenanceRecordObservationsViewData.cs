using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditMaintenanceRecordObservationsViewData : EditAttributesViewData
    {
        public EditMaintenanceRecordObservationsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, CustomAttributeTypePurpose customAttributeTypePurpose, EFModels.Entities.MaintenanceRecord maintenanceRecord, bool isSubForm, bool missingRequiredAttributes) : base(httpContext, linkGenerator, currentPerson, treatmentBMP, customAttributeTypePurpose, isSubForm, missingRequiredAttributes)
        {
            PageTitle = "Edit Maintenance Record Observations";
            ParentDetailUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, c => c.Detail(maintenanceRecord));
        }
    }
}