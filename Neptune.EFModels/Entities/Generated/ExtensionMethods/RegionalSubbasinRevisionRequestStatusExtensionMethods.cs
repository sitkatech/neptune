//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequestStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinRevisionRequestStatusExtensionMethods
    {

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