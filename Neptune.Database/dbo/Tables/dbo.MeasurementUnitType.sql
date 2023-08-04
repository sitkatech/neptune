CREATE TABLE [dbo].[MeasurementUnitType](
	[MeasurementUnitTypeID] [int] NOT NULL CONSTRAINT [PK_MeasurementUnitType_MeasurementUnitTypeID] PRIMARY KEY,
	[MeasurementUnitTypeName] [varchar](100) CONSTRAINT [AK_MeasurementUnitType_MeasurementUnitTypeName] UNIQUE,
	[MeasurementUnitTypeDisplayName] [varchar](100) CONSTRAINT [AK_MeasurementUnitType_MeasurementUnitTypeDisplayName] UNIQUE,
	[LegendDisplayName] [varchar](50) NULL,
	[SingularDisplayName] [varchar](50) NULL,
	[NumberOfSignificantDigits] [int] NOT NULL,
	[IncludeSpaceBeforeLegendLabel] [bit] NOT NULL
)
