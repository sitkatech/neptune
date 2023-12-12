//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MeasurementUnitType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MeasurementUnitTypeExtensionMethods
    {
        public static MeasurementUnitTypeSimpleDto AsSimpleDto(this MeasurementUnitType measurementUnitType)
        {
            var dto = new MeasurementUnitTypeSimpleDto()
            {
                MeasurementUnitTypeID = measurementUnitType.MeasurementUnitTypeID,
                MeasurementUnitTypeName = measurementUnitType.MeasurementUnitTypeName,
                MeasurementUnitTypeDisplayName = measurementUnitType.MeasurementUnitTypeDisplayName,
                LegendDisplayName = measurementUnitType.LegendDisplayName,
                SingularDisplayName = measurementUnitType.SingularDisplayName,
                NumberOfSignificantDigits = measurementUnitType.NumberOfSignificantDigits,
                IncludeSpaceBeforeLegendLabel = measurementUnitType.IncludeSpaceBeforeLegendLabel
            };
            return dto;
        }
    }
}