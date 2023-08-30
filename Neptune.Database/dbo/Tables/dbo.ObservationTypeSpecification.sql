CREATE TABLE [dbo].[ObservationTypeSpecification](
	[ObservationTypeSpecificationID] [int] NOT NULL CONSTRAINT [PK_ObservationTypeSpecification_ObservationTypeSpecificationID] PRIMARY KEY,
	[ObservationTypeSpecificationName] [varchar](100) CONSTRAINT [AK_ObservationTypeSpecification_ObservationTypeSpecificationName] UNIQUE,
	[ObservationTypeSpecificationDisplayName] [varchar](100) CONSTRAINT [AK_ObservationTypeSpecification_ObservationTypeSpecificationDisplayName] UNIQUE,
	[ObservationTypeCollectionMethodID] [int] NOT NULL CONSTRAINT [FK_ObservationTypeSpecification_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID] FOREIGN KEY REFERENCES [dbo].[ObservationTypeCollectionMethod] ([ObservationTypeCollectionMethodID]),
	[ObservationTargetTypeID] [int] NOT NULL CONSTRAINT [FK_ObservationTypeSpecification_ObservationTargetType_ObservationTargetTypeID] FOREIGN KEY REFERENCES [dbo].[ObservationTargetType] ([ObservationTargetTypeID]),
	[ObservationThresholdTypeID] [int] NOT NULL CONSTRAINT [FK_ObservationTypeSpecification_ObservationThresholdType_ObservationThresholdTypeID] FOREIGN KEY REFERENCES [dbo].[ObservationThresholdType] ([ObservationThresholdTypeID])
)