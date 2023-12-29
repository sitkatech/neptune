//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]
namespace Neptune.EFModels.Entities
{
    public partial class OrganizationType : IHavePrimaryKey
    {
        public int PrimaryKey => OrganizationTypeID;


        public static class FieldLengths
        {
            public const int OrganizationTypeName = 200;
            public const int OrganizationTypeAbbreviation = 100;
            public const int LegendColor = 10;
        }
    }
}