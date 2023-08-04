//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPType
    {
        public TreatmentBMPModelingType TreatmentBMPModelingType => TreatmentBMPModelingTypeID.HasValue ? TreatmentBMPModelingType.AllLookupDictionary[TreatmentBMPModelingTypeID.Value] : null;
    }
}