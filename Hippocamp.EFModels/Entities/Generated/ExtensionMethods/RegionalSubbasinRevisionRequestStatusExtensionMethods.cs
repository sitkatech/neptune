//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequestStatus]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class RegionalSubbasinRevisionRequestStatusExtensionMethods
    {
        public static RegionalSubbasinRevisionRequestStatusDto AsDto(this RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus)
        {
            var regionalSubbasinRevisionRequestStatusDto = new RegionalSubbasinRevisionRequestStatusDto()
            {
                RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusID,
                RegionalSubbasinRevisionRequestStatusName = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusName,
                RegionalSubbasinRevisionRequestStatusDisplayName = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusDisplayName
            };
            DoCustomMappings(regionalSubbasinRevisionRequestStatus, regionalSubbasinRevisionRequestStatusDto);
            return regionalSubbasinRevisionRequestStatusDto;
        }

        static partial void DoCustomMappings(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus, RegionalSubbasinRevisionRequestStatusDto regionalSubbasinRevisionRequestStatusDto);

        public static RegionalSubbasinRevisionRequestStatusSimpleDto AsSimpleDto(this RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus)
        {
            var regionalSubbasinRevisionRequestStatusSimpleDto = new RegionalSubbasinRevisionRequestStatusSimpleDto()
            {
                RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusID,
                RegionalSubbasinRevisionRequestStatusName = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusName,
                RegionalSubbasinRevisionRequestStatusDisplayName = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusDisplayName
            };
            DoCustomSimpleDtoMappings(regionalSubbasinRevisionRequestStatus, regionalSubbasinRevisionRequestStatusSimpleDto);
            return regionalSubbasinRevisionRequestStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus, RegionalSubbasinRevisionRequestStatusSimpleDto regionalSubbasinRevisionRequestStatusSimpleDto);
    }
}