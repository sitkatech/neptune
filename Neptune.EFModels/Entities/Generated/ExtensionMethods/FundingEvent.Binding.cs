//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
namespace Neptune.EFModels.Entities
{
    public partial class FundingEvent
    {
        public FundingEventType FundingEventType => FundingEventType.AllLookupDictionary[FundingEventTypeID];
    }
}