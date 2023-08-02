CREATE TABLE [dbo].[MeasurementUnitType](
	[MeasurementUnitTypeID] [int] NOT NULL,
	[MeasurementUnitTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MeasurementUnitTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LegendDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SingularDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NumberOfSignificantDigits] [int] NOT NULL,
	[IncludeSpaceBeforeLegendLabel] [bit] NOT NULL,
 CONSTRAINT [PK_MeasurementUnitType_MeasurementUnitTypeID] PRIMARY KEY CLUSTERED 
(
	[MeasurementUnitTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_MeasurementUnitType_MeasurementUnitTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[MeasurementUnitTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_MeasurementUnitType_MeasurementUnitTypeName] UNIQUE NONCLUSTERED 
(
	[MeasurementUnitTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
