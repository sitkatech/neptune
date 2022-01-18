//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class FundingEventExtensionMethods
    {
        public static FundingEventDto AsDto(this FundingEvent fundingEvent)
        {
            var fundingEventDto = new FundingEventDto()
            {
                FundingEventID = fundingEvent.FundingEventID,
                TreatmentBMP = fundingEvent.TreatmentBMP.AsDto(),
                FundingEventType = fundingEvent.FundingEventType.AsDto(),
                Year = fundingEvent.Year,
                Description = fundingEvent.Description
            };
            DoCustomMappings(fundingEvent, fundingEventDto);
            return fundingEventDto;
        }

        static partial void DoCustomMappings(FundingEvent fundingEvent, FundingEventDto fundingEventDto);

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