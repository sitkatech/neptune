using Neptune.Common;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class RegionalSubbasinExtensionMethods
{
    public static double? GetRegionalSubbasinArea(this RegionalSubbasin regionalSubbasin)
    {
        return regionalSubbasin?.CatchmentGeometry.Area != null
            ? Math.Round(regionalSubbasin.CatchmentGeometry.Area * Constants.SquareMetersToAcres, 2)
            : null;
    }

    public static RegionalSubbasinDto AsDto(this RegionalSubbasin entity)
    {
        return new RegionalSubbasinDto
        {
            RegionalSubbasinID = entity.RegionalSubbasinID,
            OCSurveyCatchmentID = entity.OCSurveyCatchmentID,
            OCSurveyDownstreamCatchmentID = entity.OCSurveyDownstreamCatchmentID,
            DownstreamRegionalSubbasinID = entity.OCSurveyDownstreamCatchment?.RegionalSubbasinID,
            Watershed = entity.Watershed,
            DrainID = entity.DrainID,
            DisplayName = entity.GetDisplayName(),
            Area = entity.CatchmentGeometry?.Area * Constants.SquareMetersToAcres
        };
    }

    public static RegionalSubbasinSimpleDto AsSimpleDto(this RegionalSubbasin entity)
    {
        return new RegionalSubbasinSimpleDto
        {
            RegionalSubbasinID = entity.RegionalSubbasinID,
            DisplayName = entity.GetDisplayName()
        };
    }

    public static void UpdateFromUpsertDto(this RegionalSubbasin entity, RegionalSubbasinUpsertDto dto)
    {
        entity.OCSurveyCatchmentID = dto.OCSurveyCatchmentID;
        entity.OCSurveyDownstreamCatchmentID = dto.OCSurveyDownstreamCatchmentID;
        entity.Watershed = dto.Watershed;
        entity.DrainID = dto.DrainID;
    }

    public static RegionalSubbasin AsEntity(this RegionalSubbasinUpsertDto dto)
    {
        return new RegionalSubbasin
        {
            OCSurveyCatchmentID = dto.OCSurveyCatchmentID,
            OCSurveyDownstreamCatchmentID = dto.OCSurveyDownstreamCatchmentID,
            Watershed = dto.Watershed,
            DrainID = dto.DrainID
        };
    }
}