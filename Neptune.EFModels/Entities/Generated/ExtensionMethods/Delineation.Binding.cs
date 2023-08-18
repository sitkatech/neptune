//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]
namespace Neptune.EFModels.Entities
{
    public partial class Delineation : IHavePrimaryKey
    {
        public int PrimaryKey => DelineationID;
        public DelineationType DelineationType => DelineationType.AllLookupDictionary[DelineationTypeID];
    }
}