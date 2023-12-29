//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
namespace Neptune.EFModels.Entities
{
    public partial class NeptunePage : IHavePrimaryKey
    {
        public int PrimaryKey => NeptunePageID;
        public NeptunePageType NeptunePageType => NeptunePageType.AllLookupDictionary[NeptunePageTypeID];

        public static class FieldLengths
        {

        }
    }
}