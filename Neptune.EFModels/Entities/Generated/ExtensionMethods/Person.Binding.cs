//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Person]
namespace Neptune.EFModels.Entities
{
    public partial class Person : IHavePrimaryKey
    {
        public int PrimaryKey => PersonID;
        public Role Role => Role.AllLookupDictionary[RoleID];
    }
}