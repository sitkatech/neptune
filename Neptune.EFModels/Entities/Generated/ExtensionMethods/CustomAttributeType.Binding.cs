//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
namespace Neptune.EFModels.Entities
{
    public partial class CustomAttributeType : IHavePrimaryKey
    {
        public int PrimaryKey => CustomAttributeTypeID;
        public CustomAttributeDataType CustomAttributeDataType => CustomAttributeDataType.AllLookupDictionary[CustomAttributeDataTypeID];
        public MeasurementUnitType MeasurementUnitType => MeasurementUnitTypeID.HasValue ? MeasurementUnitType.AllLookupDictionary[MeasurementUnitTypeID.Value] : null;
        public CustomAttributeTypePurpose CustomAttributeTypePurpose => CustomAttributeTypePurpose.AllLookupDictionary[CustomAttributeTypePurposeID];

        public static class FieldLengths
        {
            public const int CustomAttributeTypeName = 100;
            public const int CustomAttributeTypeDescription = 200;
        }
    }
}