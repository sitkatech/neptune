CREATE TABLE [dbo].[ObservationTypeCollectionMethod](
	[ObservationTypeCollectionMethodID] [int] NOT NULL CONSTRAINT [PK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID] PRIMARY KEY,
	[ObservationTypeCollectionMethodName] [varchar](100) CONSTRAINT [AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodName] UNIQUE,
	[ObservationTypeCollectionMethodDisplayName] [varchar](100) CONSTRAINT [AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodDisplayName] UNIQUE,
	[ObservationTypeCollectionMethodDescription] [varchar](max)
)
