//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestLog]
namespace Neptune.EFModels.Entities
{
    public partial class SupportRequestLog : IHavePrimaryKey
    {
        public int PrimaryKey => SupportRequestLogID;
        public SupportRequestType SupportRequestType => SupportRequestType.AllLookupDictionary[SupportRequestTypeID];
    }
}