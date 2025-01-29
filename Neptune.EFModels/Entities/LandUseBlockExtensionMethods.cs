using Neptune.Common;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Algorithm;

namespace Neptune.EFModels.Entities;

public static partial class LandUseBlockExtensionMethods
{
    public static LandUseBlockGridDto AsGridDto(this LandUseBlock landUseBlock)
    {
        var dto = new LandUseBlockGridDto()
        {
            LandUseBlockID = landUseBlock.LandUseBlockID,
            PriorityLandUseTypeName = landUseBlock.PriorityLandUseType?.PriorityLandUseTypeDisplayName,
            LandUseDescription = landUseBlock.LandUseDescription,
            TrashGenerationRate = landUseBlock.TrashGenerationRate,
            LandUseForTGR = landUseBlock.LandUseForTGR,
            MedianHouseholdIncomeResidential = landUseBlock.MedianHouseholdIncomeResidential,
            MedianHouseholdIncomeRetail = landUseBlock.MedianHouseholdIncomeRetail,
            StormwaterJurisdictionID = landUseBlock.StormwaterJurisdictionID,
            StormwaterJurisdictionName = landUseBlock.StormwaterJurisdiction.GetOrganizationDisplayName(),
            PermitTypeName = landUseBlock.PermitType.PermitTypeDisplayName,
            Area = landUseBlock.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres,
            TrashGeneratingArea = landUseBlock.TrashGeneratingUnits.Sum(y => y.TrashGeneratingUnitGeometry.Area) * Constants.SquareMetersToAcres,
        };
        return dto;
    }
}