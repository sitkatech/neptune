SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreliminarySourceIdentificationType](
	[PreliminarySourceIdentificationTypeID] [int] NOT NULL,
	[PreliminarySourceIdentificationTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreliminarySourceIdentificationTypeDisplayName] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreliminarySourceIdentificationCategoryID] [int] NOT NULL,
 CONSTRAINT [PK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeID] PRIMARY KEY CLUSTERED 
(
	[PreliminarySourceIdentificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[PreliminarySourceIdentificationTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeName] UNIQUE NONCLUSTERED 
(
	[PreliminarySourceIdentificationTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PreliminarySourceIdentificationType]  WITH CHECK ADD  CONSTRAINT [FK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID] FOREIGN KEY([PreliminarySourceIdentificationCategoryID])
REFERENCES [dbo].[PreliminarySourceIdentificationCategory] ([PreliminarySourceIdentificationCategoryID])
GO
ALTER TABLE [dbo].[PreliminarySourceIdentificationType] CHECK CONSTRAINT [FK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID]