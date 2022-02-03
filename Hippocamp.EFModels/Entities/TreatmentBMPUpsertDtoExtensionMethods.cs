using Hippocamp.Models.DataTransferObjects;


namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPUpsertDtoExtensionMethods
    {
        public static TreatmentBMPUpsertDto AsUpsertDto(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPUpsertDto = new TreatmentBMPUpsertDto()
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
                WatershedName = treatmentBMP.Watershed.WatershedName,
                StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName,
                Longitude = treatmentBMP.Longitude,
                Latitude = treatmentBMP.Latitude,
                Notes = treatmentBMP.Notes
            };

            return treatmentBMPUpsertDto;
        }
    }
}