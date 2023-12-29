using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class MaintenanceRecordObservationExtensionMethods
{
    public static CustomAttributeUpsertDto AsUpsertDto(this MaintenanceRecordObservation maintenanceRecordObservation)
    {
        var customAttributeUpsertDto = new CustomAttributeUpsertDto(
            maintenanceRecordObservation.TreatmentBMPTypeCustomAttributeTypeID, maintenanceRecordObservation.CustomAttributeTypeID,
            maintenanceRecordObservation.MaintenanceRecordObservationValues.Select(x => x.ObservationValue).ToList());
        return customAttributeUpsertDto;
    }
}