//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinStagingExtensionMethods
    {
        public static RegionalSubbasinStagingSimpleDto AsSimpleDto(this RegionalSubbasinStaging regionalSubbasinStaging)
        {
            var dto = new RegionalSubbasinStagingSimpleDto()
            {
                RegionalSubbasinStagingID = regionalSubbasinStaging.RegionalSubbasinStagingID,
                DrainID = regionalSubbasinStaging.DrainID,
                Watershed = regionalSubbasinStaging.Watershed,
                OCSurveyCatchmentID = regionalSubbasinStaging.OCSurveyCatchmentID,
                OCSurveyDownstreamCatchmentID = regionalSubbasinStaging.OCSurveyDownstreamCatchmentID
            };
            return dto;
        }
    }
}