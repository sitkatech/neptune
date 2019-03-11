SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationTypeSpecification](
	[ObservationTypeSpecificationID] [int] NOT NULL,
	[ObservationTypeSpecificationName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeSpecificationDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
	[ObservationTypeCollectionMethodID] [int] NOT NULL,
	[ObservationTargetTypeID] [int] NOT NULL,
	[ObservationThresholdTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ObservationTypeSpecification_ObservationTypeSpecificationID] PRIMARY KEY CLUSTERED 
(
	[ObservationTypeSpecificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTypeSpecification_ObservationTypeSpecificationDisplayName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeSpecificationDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ObservationTypeSpecification_ObservationTypeSpecificationName] UNIQUE NONCLUSTERED 
(
	[ObservationTypeSpecificationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ObservationTypeSpecification]  WITH CHECK ADD  CONSTRAINT [FK_ObservationTypeSpecification_ObservationTargetType_ObservationTargetTypeID] FOREIGN KEY([ObservationTargetTypeID])
REFERENCES [dbo].[ObservationTargetType] ([ObservationTargetTypeID])
GO
ALTER TABLE [dbo].[ObservationTypeSpecification] CHECK CONSTRAINT [FK_ObservationTypeSpecification_ObservationTargetType_ObservationTargetTypeID]
GO
ALTER TABLE [dbo].[ObservationTypeSpecification]  WITH CHECK ADD  CONSTRAINT [FK_ObservationTypeSpecification_ObservationThresholdType_ObservationThresholdTypeID] FOREIGN KEY([ObservationThresholdTypeID])
REFERENCES [dbo].[ObservationThresholdType] ([ObservationThresholdTypeID])
GO
ALTER TABLE [dbo].[ObservationTypeSpecification] CHECK CONSTRAINT [FK_ObservationTypeSpecification_ObservationThresholdType_ObservationThresholdTypeID]
GO
ALTER TABLE [dbo].[ObservationTypeSpecification]  WITH CHECK ADD  CONSTRAINT [FK_ObservationTypeSpecification_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID] FOREIGN KEY([ObservationTypeCollectionMethodID])
REFERENCES [dbo].[ObservationTypeCollectionMethod] ([ObservationTypeCollectionMethodID])
GO
ALTER TABLE [dbo].[ObservationTypeSpecification] CHECK CONSTRAINT [FK_ObservationTypeSpecification_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodID]