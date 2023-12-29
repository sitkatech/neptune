//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPType : IHavePrimaryKey
    {
        public int PrimaryKey => TreatmentBMPTypeID;
        public TreatmentBMPModelingType TreatmentBMPModelingType => TreatmentBMPModelingTypeID.HasValue ? TreatmentBMPModelingType.AllLookupDictionary[TreatmentBMPModelingTypeID.Value] : null;

        public static class FieldLengths
        {
            public const int TreatmentBMPTypeName = 100;
            public const int TreatmentBMPTypeDescription = 1000;
        }
    }
}