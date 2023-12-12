//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FundingEventExtensionMethods
    {

        public static FundingEventSimpleDto AsSimpleDto(this FundingEvent fundingEvent)
        {
            var fundingEventSimpleDto = new FundingEventSimpleDto()
            {
                FundingEventID = fundingEvent.FundingEventID,
                TreatmentBMPID = fundingEvent.TreatmentBMPID,
                FundingEventTypeID = fundingEvent.FundingEventTypeID,
                Year = fundingEvent.Year,
                Description = fundingEvent.Description
            };
            DoCustomSimpleDtoMappings(fundingEvent, fundingEventSimpleDto);
            return fundingEventSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FundingEvent fundingEvent, FundingEventSimpleDto fundingEventSimpleDto);
    }
}