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
            var measurementUnitTypeSimpleDto = new MeasurementUnitTypeSimpleDto()
            {
                MeasurementUnitTypeID = measurementUnitType.MeasurementUnitTypeID,
                MeasurementUnitTypeName = measurementUnitType.MeasurementUnitTypeName,
                MeasurementUnitTypeDisplayName = measurementUnitType.MeasurementUnitTypeDisplayName,
                LegendDisplayName = measurementUnitType.LegendDisplayName,
                SingularDisplayName = measurementUnitType.SingularDisplayName,
                NumberOfSignificantDigits = measurementUnitType.NumberOfSignificantDigits,
                IncludeSpaceBeforeLegendLabel = measurementUnitType.IncludeSpaceBeforeLegendLabel
            };
            DoCustomSimpleDtoMappings(measurementUnitType, measurementUnitTypeSimpleDto);
            return measurementUnitTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MeasurementUnitType measurementUnitType, MeasurementUnitTypeSimpleDto measurementUnitTypeSimpleDto);
    }
}