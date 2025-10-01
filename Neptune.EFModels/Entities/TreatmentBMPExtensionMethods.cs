using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPExtensionMethods
{
    public static TreatmentBMPDisplayDto AsDisplayDto(this TreatmentBMP treatmentBMP, vTreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
    {
        var treatmentBmpDisplayDto = new TreatmentBMPDisplayDto()
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            DisplayName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
            ProjectID = treatmentBMP.ProjectID,
            InventoryIsVerified = treatmentBMP.InventoryIsVerified,
            Longitude = treatmentBMP.LocationPoint4326.Coordinate.X,
            Latitude = treatmentBMP.LocationPoint4326.Coordinate.Y,
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPModelingAttribute = treatmentBMPModelingAttribute != null ? treatmentBMPModelingAttribute.AsDto() : null,
            CustomAttributes = treatmentBMP.CustomAttributes?.Select(x => x.AsUpsertDto()).ToList(),
            IsFullyParameterized = treatmentBMP.IsFullyParameterized(treatmentBMPModelingAttribute),
            WatershedName = treatmentBMP.Watershed?.WatershedName,
            Notes = treatmentBMP.Notes
        };
        return treatmentBmpDisplayDto;
    }

    public static TreatmentBMPUpsertDto AsUpsertDtoWithModelingAttributes(this TreatmentBMP treatmentBMP, vTreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
    {
        var treatmentBMPUpsertDto = new TreatmentBMPUpsertDto()
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
            TreatmentBMPModelingTypeID = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID,
            WatershedName = treatmentBMP.Watershed?.WatershedName,
            StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName,
            Longitude = treatmentBMP.LocationPoint4326?.Coordinate.X,
            Latitude = treatmentBMP.LocationPoint4326?.Coordinate.Y,
            Notes = treatmentBMP.Notes,
            ModelingAttributes = treatmentBMP.CustomAttributes.Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == (int)CustomAttributeTypePurposeEnum.Modeling).Select(x => x.AsUpsertDto()).ToList(),
            AreAllModelingAttributesComplete = !treatmentBMP.TreatmentBMPType.MissingModelingAttributes(treatmentBMPModelingAttribute).Any(),
            IsFullyParameterized = treatmentBMP.IsFullyParameterized(treatmentBMPModelingAttribute)
        };

        return treatmentBMPUpsertDto;
    }


    public static WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto AsWaterQualityManagementPlanVerifyTreatmentBMPSimpleDto(this TreatmentBMP treatmentBMP)
    {
        var waterQualityManagementPlanVerifyTreatmentBMPSimpleDto = new WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()
        {
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPType = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
        };
        //var mostRecentFieldVisit = treatmentBMP.FieldVisit.Where(x => x.FieldVisitStatus == FieldVisitStatus.Complete).OrderByDescending(x => x.VisitDate).FirstOrDefault();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisiLastVisitedtDate = mostRecentFieldVisit?.VisitDate.ToShortDateString();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisitMostRecentScore = mostRecentFieldVisit?.GetPostMaintenanceAssessment() != null ? mostRecentFieldVisit.GetPostMaintenanceAssessment().FormattedScore() : mostRecentFieldVisit?.GetInitialAssessment()?.FormattedScore();

        return waterQualityManagementPlanVerifyTreatmentBMPSimpleDto;
    }

    public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP, Delineation? delineation, vTreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
    {
        // Planning BMPs don't need verified delineations
        // assumes the delineation passed in is the from the "upstreamest" BMP
        if (treatmentBMP.ProjectID == null && !(delineation?.IsVerified ?? false))
        {
            return false;
        }

        var treatmentBMPType = treatmentBMP.TreatmentBMPType;
        return !treatmentBMPType.MissingModelingAttributes(treatmentBMPModelingAttribute).Any();
    }

    public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP, vTreatmentBMPModelingAttribute bmpModelingAttributes)
    {
        if (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID == null)
        {
            return false;
        }

        if (treatmentBMP.Delineation == null)
        {
            return false;
        }

        return !treatmentBMP.TreatmentBMPType.MissingModelingAttributes(bmpModelingAttributes).Any();
    }

    public static void SetTreatmentBMPPointInPolygonDataByLocationPoint(this TreatmentBMP treatmentBMP,
        Geometry locationPoint, NeptuneDbContext dbContext)
    {
        treatmentBMP.WatershedID = dbContext.Watersheds.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.WatershedGeometry))?.WatershedID;
        treatmentBMP.ModelBasinID = dbContext.ModelBasins.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.ModelBasinGeometry))?.ModelBasinID;
        treatmentBMP.PrecipitationZoneID = dbContext.PrecipitationZones.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.PrecipitationZoneGeometry))?.PrecipitationZoneID;
        treatmentBMP.RegionalSubbasinID = dbContext.RegionalSubbasins.AsNoTracking()
            .FirstOrDefault(x => locationPoint.Intersects(x.CatchmentGeometry))?.RegionalSubbasinID;
    }

    public static TreatmentBMPDto AsDto(this TreatmentBMP treatmentBMP)
    {
        return new TreatmentBMPDto
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType?.TreatmentBMPTypeName,
            StormwaterJurisdictionID = treatmentBMP.StormwaterJurisdictionID,
            StormwaterJurisdictionName = treatmentBMP.StormwaterJurisdiction?.Organization?.OrganizationName,
            OwnerOrganizationID = treatmentBMP.OwnerOrganizationID,
            OwnerOrganizationName = treatmentBMP.OwnerOrganization?.OrganizationName,
            YearBuilt = treatmentBMP.YearBuilt,
            Notes = treatmentBMP.Notes,
            InventoryIsVerified = treatmentBMP.InventoryIsVerified,
            ProjectID = treatmentBMP.ProjectID,
            Latitude = treatmentBMP.LocationPoint4326?.Coordinate.Y,
            Longitude = treatmentBMP.LocationPoint4326?.Coordinate.X,
            SystemOfRecordID = treatmentBMP.SystemOfRecordID,
            WaterQualityManagementPlanID = treatmentBMP.WaterQualityManagementPlanID,
            TreatmentBMPLifespanEndDate = treatmentBMP.TreatmentBMPLifespanEndDate,
            RequiredFieldVisitsPerYear = treatmentBMP.RequiredFieldVisitsPerYear,
            RequiredPostStormFieldVisitsPerYear = treatmentBMP.RequiredPostStormFieldVisitsPerYear,
            DateOfLastInventoryVerification = treatmentBMP.DateOfLastInventoryVerification,
            InventoryVerifiedByPersonID = treatmentBMP.InventoryVerifiedByPersonID,
            InventoryLastChangedDate = treatmentBMP.InventoryLastChangedDate,
            TrashCaptureStatusTypeID = treatmentBMP.TrashCaptureStatusTypeID,
            SizingBasisTypeID = treatmentBMP.SizingBasisTypeID,
            TrashCaptureEffectiveness = treatmentBMP.TrashCaptureEffectiveness?.ToString(),
            WatershedID = treatmentBMP.WatershedID,
            ModelBasinID = treatmentBMP.ModelBasinID,
            PrecipitationZoneID = treatmentBMP.PrecipitationZoneID,
            UpstreamBMPID = treatmentBMP.UpstreamBMPID,
            RegionalSubbasinID = treatmentBMP.RegionalSubbasinID,
            LastNereidLogID = treatmentBMP.LastNereidLogID,
            UpstreamBMP = treatmentBMP.UpstreamBMP?.AsDto(),
            RegionalSubbasinRevisionRequests = treatmentBMP.RegionalSubbasinRevisionRequests?.Select(x => x.AsDto()).ToList(),
            Watershed = treatmentBMP.Watershed?.AsDto(),
            Project = treatmentBMP.Project?.AsDto(),
//            OwnerOrganization = treatmentBMP.OwnerOrganization?.AsDto(),
//            StormwaterJurisdiction = treatmentBMP.StormwaterJurisdiction?.AsDto(),
            MaintenanceRecords = treatmentBMP.MaintenanceRecords?.Select(x => x.AsDto()).ToList(),
            TreatmentBMPAssessments = treatmentBMP.TreatmentBMPAssessments?.Select(x => x.AsDto()).ToList(),
            TreatmentBMPBenchmarkAndThresholds = treatmentBMP.TreatmentBMPBenchmarkAndThresholds?.Select(x => x.AsDto()).ToList(),
            NereidResults = treatmentBMP.NereidResults?.Select(x => x.AsDto()).ToList(),
            ProjectNereidResults = treatmentBMP.ProjectNereidResults?.Select(x => x.AsDto()).ToList(),
            WaterQualityManagementPlanVerifyTreatmentBMPs = treatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPs?.Select(x => x.AsDto()).ToList(),
            SizingBasisType = treatmentBMP.SizingBasisType != null ? new SizingBasisTypeDto
            {
                SizingBasisTypeID = treatmentBMP.SizingBasisType.SizingBasisTypeID,
                SizingBasisTypeName = treatmentBMP.SizingBasisType.SizingBasisTypeName,
                SizingBasisTypeDisplayName = treatmentBMP.SizingBasisType.SizingBasisTypeDisplayName
            } : null,
            TrashCaptureStatusType = treatmentBMP.TrashCaptureStatusType != null ? new TrashCaptureStatusTypeDto
            {
                TrashCaptureStatusTypeID = treatmentBMP.TrashCaptureStatusType.TrashCaptureStatusTypeID,
                TrashCaptureStatusTypeName = treatmentBMP.TrashCaptureStatusType.TrashCaptureStatusTypeName,
                TrashCaptureStatusTypeDisplayName = treatmentBMP.TrashCaptureStatusType.TrashCaptureStatusTypeDisplayName
            } : null,
            TreatmentBMPLifespanType = treatmentBMP.TreatmentBMPLifespanType != null ? new TreatmentBMPLifeSpanTypeDto
            {
                TreatmentBMPLifeSpanTypeID = treatmentBMP.TreatmentBMPLifespanType.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifeSpanTypeName = treatmentBMP.TreatmentBMPLifespanType.TreatmentBMPLifespanTypeName,
                TreatmentBMPLifeSpanTypeDisplayName = treatmentBMP.TreatmentBMPLifespanType.TreatmentBMPLifespanTypeDisplayName
            } : null,
        };
    }
}