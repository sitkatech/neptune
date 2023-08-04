CREATE TABLE [dbo].[PreliminarySourceIdentificationType](
	[PreliminarySourceIdentificationTypeID] [int] NOT NULL CONSTRAINT [PK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeID] PRIMARY KEY,
	[PreliminarySourceIdentificationTypeName] [varchar](100) CONSTRAINT [AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeName] UNIQUE,
	[PreliminarySourceIdentificationTypeDisplayName] [varchar](100) CONSTRAINT [AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeDisplayName] UNIQUE,
	[PreliminarySourceIdentificationCategoryID] [int] NOT NULL CONSTRAINT [FK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID] FOREIGN KEY REFERENCES [dbo].[PreliminarySourceIdentificationCategory] ([PreliminarySourceIdentificationCategoryID])
)