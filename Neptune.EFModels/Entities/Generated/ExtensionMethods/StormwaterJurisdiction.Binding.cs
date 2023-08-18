//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
namespace Neptune.EFModels.Entities
{
    public partial class StormwaterJurisdiction : IHavePrimaryKey
    {
        public int PrimaryKey => StormwaterJurisdictionID;
        public StormwaterJurisdictionPublicBMPVisibilityType StormwaterJurisdictionPublicBMPVisibilityType => StormwaterJurisdictionPublicBMPVisibilityType.AllLookupDictionary[StormwaterJurisdictionPublicBMPVisibilityTypeID];
        public StormwaterJurisdictionPublicWQMPVisibilityType StormwaterJurisdictionPublicWQMPVisibilityType => StormwaterJurisdictionPublicWQMPVisibilityType.AllLookupDictionary[StormwaterJurisdictionPublicWQMPVisibilityTypeID];
    }
}