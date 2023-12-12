//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasin]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinExtensionMethods
    {

        public static RegionalSubbasinSimpleDto AsSimpleDto(this RegionalSubbasin regionalSubbasin)
        {
            var regionalSubbasinSimpleDto = new RegionalSubbasinSimpleDto()
            {
                RegionalSubbasinID = regionalSubbasin.RegionalSubbasinID,
                DrainID = regionalSubbasin.DrainID,
                Watershed = regionalSubbasin.Watershed,
                OCSurveyCatchmentID = regionalSubbasin.OCSurveyCatchmentID,
                OCSurveyDownstreamCatchmentID = regionalSubbasin.OCSurveyDownstreamCatchmentID,
                LastUpdate = regionalSubbasin.LastUpdate,
                IsWaitingForLGURefresh = regionalSubbasin.IsWaitingForLGURefresh,
                IsInModelBasin = regionalSubbasin.IsInModelBasin,
                ModelBasinID = regionalSubbasin.ModelBasinID
            };
            DoCustomSimpleDtoMappings(regionalSubbasin, regionalSubbasinSimpleDto);
            return regionalSubbasinSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(RegionalSubbasin regionalSubbasin, RegionalSubbasinSimpleDto regionalSubbasinSimpleDto);
    }
}