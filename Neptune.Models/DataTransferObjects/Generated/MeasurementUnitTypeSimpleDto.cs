//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MeasurementUnitType]

namespace Neptune.Models.DataTransferObjects
{
    public partial class MeasurementUnitTypeSimpleDto
    {
        public int MeasurementUnitTypeID { get; set; }
        public string MeasurementUnitTypeName { get; set; }
        public string MeasurementUnitTypeDisplayName { get; set; }
        public string LegendDisplayName { get; set; }
        public string SingularDisplayName { get; set; }
        public int NumberOfSignificantDigits { get; set; }
        public bool IncludeSpaceBeforeLegendLabel { get; set; }
    }
}