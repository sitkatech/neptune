//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasinRevisionRequest : IHavePrimaryKey
    {
        public int PrimaryKey => RegionalSubbasinRevisionRequestID;
        public RegionalSubbasinRevisionRequestStatus RegionalSubbasinRevisionRequestStatus => RegionalSubbasinRevisionRequestStatus.AllLookupDictionary[RegionalSubbasinRevisionRequestStatusID];

        public static class FieldLengths
        {

        }
    }
}