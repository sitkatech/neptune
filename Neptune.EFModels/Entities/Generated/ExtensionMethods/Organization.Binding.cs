//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]
namespace Neptune.EFModels.Entities
{
    public partial class Organization : IHavePrimaryKey
    {
        public int PrimaryKey => OrganizationID;


        public static class FieldLengths
        {
            public const int OrganizationName = 200;
            public const int OrganizationShortName = 50;
            public const int OrganizationUrl = 200;
        }
    }
}