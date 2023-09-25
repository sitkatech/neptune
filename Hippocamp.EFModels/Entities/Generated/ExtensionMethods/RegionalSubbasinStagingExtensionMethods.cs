//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinStagingExtensionMethods
    {
        public static RegionalSubbasinStagingDto AsDto(this RegionalSubbasinStaging regionalSubbasinStaging)
        {
            var regionalSubbasinStagingDto = new RegionalSubbasinStagingDto()
            {
                RegionalSubbasinStagingID = regionalSubbasinStaging.RegionalSubbasinStagingID,
                DrainID = regionalSubbasinStaging.DrainID,
                Watershed = regionalSubbasinStaging.Watershed,
                OCSurveyCatchmentID = regionalSubbasinStaging.OCSurveyCatchmentID,
                OCSurveyDownstreamCatchmentID = regionalSubbasinStaging.OCSurveyDownstreamCatchmentID
            };
            DoCustomMappings(regionalSubbasinStaging, regionalSubbasinStagingDto);
            return regionalSubbasinStagingDto;
        }

        static partial void DoCustomMappings(RegionalSubbasinStaging regionalSubbasinStaging, RegionalSubbasinStagingDto regionalSubbasinStagingDto);

        public static RegionalSubbasinStagingSimpleDto AsSimpleDto(this RegionalSubbasinStaging regionalSubbasinStaging)
        {
            var regionalSubbasinStagingSimpleDto = new RegionalSubbasinStagingSimpleDto()
            {
                RegionalSubbasinStagingID = regionalSubbasinStaging.RegionalSubbasinStagingID,
                DrainID = regionalSubbasinStaging.DrainID,
                Watershed = regionalSubbasinStaging.Watershed,
                OCSurveyCatchmentID = regionalSubbasinStaging.OCSurveyCatchmentID,
                OCSurveyDownstreamCatchmentID = regionalSubbasinStaging.OCSurveyDownstreamCatchmentID
            };
            DoCustomSimpleDtoMappings(regionalSubbasinStaging, regionalSubbasinStagingSimpleDto);
            return regionalSubbasinStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(RegionalSubbasinStaging regionalSubbasinStaging, RegionalSubbasinStagingSimpleDto regionalSubbasinStagingSimpleDto);
    }
}