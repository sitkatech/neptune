SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationType](
	[ObservationTypeID] [int] NOT NULL,
	[ObservationTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
	[MeasurementUnitTypeID] [int] NOT NULL,
	[HasBenchmarkAndThreshold] [bit] NOT NULL,
	[ThresholdPercentDecline] [bit] NOT NULL,
	[ThresholdPercentDeviation] [bit] NOT NULL,
 CONSTRAINT [PK_ObservationType_ObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[ObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationType_ObservationTypeName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ObservationType]  WITH CHECK ADD  CONSTRAINT [FK_ObservationType_MeasurementUnitType_MeasurementUnitTypeID] FOREIGN KEY([MeasurementUnitTypeID])
REFERENCES [dbo].[MeasurementUnitType] ([MeasurementUnitTypeID])
GO
ALTER TABLE [dbo].[ObservationType] CHECK CONSTRAINT [FK_ObservationType_MeasurementUnitType_MeasurementUnitTypeID]