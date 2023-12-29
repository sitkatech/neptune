CREATE TABLE [dbo].[ObservationThresholdType](
	[ObservationThresholdTypeID] [int] NOT NULL CONSTRAINT [PK_ObservationThresholdType_ObservationThresholdTypeID] PRIMARY KEY,
	[ObservationThresholdTypeName] [varchar](100) CONSTRAINT [AK_ObservationThresholdType_ObservationThresholdTypeName] UNIQUE,
	[ObservationThresholdTypeDisplayName] [varchar](100) CONSTRAINT [AK_ObservationThresholdType_ObservationThresholdTypeDisplayName] UNIQUE
)
