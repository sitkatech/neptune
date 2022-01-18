//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPExtensionMethods
    {
        public static TreatmentBMPDto AsDto(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPDto = new TreatmentBMPDto()
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                TreatmentBMPName = treatmentBMP.TreatmentBMPName,
                TreatmentBMPType = treatmentBMP.TreatmentBMPType.AsDto(),
                StormwaterJurisdiction = treatmentBMP.StormwaterJurisdiction.AsDto(),
                Notes = treatmentBMP.Notes,
                SystemOfRecordID = treatmentBMP.SystemOfRecordID,
                YearBuilt = treatmentBMP.YearBuilt,
                OwnerOrganization = treatmentBMP.OwnerOrganization.AsDto(),
                WaterQualityManagementPlan = treatmentBMP.WaterQualityManagementPlan?.AsDto(),
                TreatmentBMPLifespanType = treatmentBMP.TreatmentBMPLifespanType?.AsDto(),
                TreatmentBMPLifespanEndDate = treatmentBMP.TreatmentBMPLifespanEndDate,
                RequiredFieldVisitsPerYear = treatmentBMP.RequiredFieldVisitsPerYear,
                RequiredPostStormFieldVisitsPerYear = treatmentBMP.RequiredPostStormFieldVisitsPerYear,
                InventoryIsVerified = treatmentBMP.InventoryIsVerified,
                DateOfLastInventoryVerification = treatmentBMP.DateOfLastInventoryVerification,
                InventoryVerifiedByPerson = treatmentBMP.InventoryVerifiedByPerson?.AsDto(),
                InventoryLastChangedDate = treatmentBMP.InventoryLastChangedDate,
                TrashCaptureStatusType = treatmentBMP.TrashCaptureStatusType.AsDto(),
                SizingBasisType = treatmentBMP.SizingBasisType.AsDto(),
                TrashCaptureEffectiveness = treatmentBMP.TrashCaptureEffectiveness,
                Watershed = treatmentBMP.Watershed?.AsDto(),
                LSPCBasin = treatmentBMP.LSPCBasin?.AsDto(),
                PrecipitationZone = treatmentBMP.PrecipitationZone?.AsDto(),
                UpstreamBMPID = treatmentBMP.UpstreamBMPID,
                RegionalSubbasinID = treatmentBMP.RegionalSubbasinID
            };
            DoCustomMappings(treatmentBMP, treatmentBMPDto);
            return treatmentBMPDto;
        }

        static partial void DoCustomMappings(TreatmentBMP treatmentBMP, TreatmentBMPDto treatmentBMPDto);

        public static TreatmentBMPSimpleDto AsSimpleDto(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPSimpleDto = new TreatmentBMPSimpleDto()
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
                LSPCBasinID = treatmentBMP.LSPCBasinID,
                PrecipitationZoneID = treatmentBMP.PrecipitationZoneID,
                UpstreamBMPID = treatmentBMP.UpstreamBMPID,
                RegionalSubbasinID = treatmentBMP.RegionalSubbasinID
            };
            DoCustomSimpleDtoMappings(treatmentBMP, treatmentBMPSimpleDto);
            return treatmentBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMP treatmentBMP, TreatmentBMPSimpleDto treatmentBMPSimpleDto);
    }
}