//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]
namespace Neptune.EFModels.Entities
{
    public partial class FundingSource : IHavePrimaryKey
    {
        public int PrimaryKey => FundingSourceID;


        public static class FieldLengths
        {
            public const int FundingSourceName = 200;
            public const int FundingSourceDescription = 500;
        }
    }
}