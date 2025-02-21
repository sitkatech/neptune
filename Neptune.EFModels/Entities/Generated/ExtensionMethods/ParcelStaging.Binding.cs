//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]
namespace Neptune.EFModels.Entities
{
    public partial class ParcelStaging : IHavePrimaryKey
    {
        public int PrimaryKey => ParcelStagingID;


        public static class FieldLengths
        {
            public const int ParcelNumber = 22;
            public const int ParcelAddress = 150;
            public const int ParcelCityState = 100;
            public const int ParcelZipCode = 5;
        }
    }
}