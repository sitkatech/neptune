CREATE TABLE [dbo].[ObservationTargetType](
	[ObservationTargetTypeID] [int] NOT NULL CONSTRAINT [PK_ObservationTargetType_ObservationTargetTypeID] PRIMARY KEY,
	[ObservationTargetTypeName] [varchar](100) CONSTRAINT [AK_ObservationTargetType_ObservationTargetTypeName] UNIQUE,
	[ObservationTargetTypeDisplayName] [varchar](100) CONSTRAINT [AK_ObservationTargetType_ObservationTargetTypeDisplayName] UNIQUE
)
