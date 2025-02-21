//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]
namespace Neptune.EFModels.Entities
{
    public partial class Parcel : IHavePrimaryKey
    {
        public int PrimaryKey => ParcelID;


        public static class FieldLengths
        {
            public const int ParcelNumber = 22;
            public const int ParcelAddress = 150;
            public const int ParcelCityState = 100;
            public const int ParcelZipCode = 5;
        }
    }
}