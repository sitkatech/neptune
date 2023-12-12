//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPExtensionMethods
    {
        public static TreatmentBMPSimpleDto AsSimpleDto(this TreatmentBMP treatmentBMP)
        {
            var dto = new TreatmentBMPSimpleDto()
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
                StormwaterJurisdictionID = treatmentBMP.StormwaterJurisdictionID,
                Notes = treatmentBMP.Notes,
                SystemOfRecordID = treatmentBMP.SystemOfRecordID,
                YearBuilt = treatmentBMP.YearBuilt,
                OwnerOrganizationID = treatmentBMP.OwnerOrganizationID,
                WaterQualityManagementPlanID = treatmentBMP.WaterQualityManagementPlanID,
                TreatmentBMPLifespanTypeID = treatmentBMP.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifespanEndDate = treatmentBMP.TreatmentBMPLifespanEndDate,
                RequiredFieldVisitsPerYear = treatmentBMP.RequiredFieldVisitsPerYear,
                RequiredPostStormFieldVisitsPerYear = treatmentBMP.RequiredPostStormFieldVisitsPerYear,
                InventoryIsVerified = treatmentBMP.InventoryIsVerified,
                DateOfLastInventoryVerification = treatmentBMP.DateOfLastInventoryVerification,
                InventoryVerifiedByPersonID = treatmentBMP.InventoryVerifiedByPersonID,
                InventoryLastChangedDate = treatmentBMP.InventoryLastChangedDate,
                TrashCaptureStatusTypeID = treatmentBMP.TrashCaptureStatusTypeID,
                SizingBasisTypeID = treatmentBMP.SizingBasisTypeID,
                TrashCaptureEffectiveness = treatmentBMP.TrashCaptureEffectiveness,
                WatershedID = treatmentBMP.WatershedID,
                ModelBasinID = treatmentBMP.ModelBasinID,
                PrecipitationZoneID = treatmentBMP.PrecipitationZoneID,
                UpstreamBMPID = treatmentBMP.UpstreamBMPID,
                RegionalSubbasinID = treatmentBMP.RegionalSubbasinID,
                ProjectID = treatmentBMP.ProjectID
            };
            return dto;
        }
    }
}