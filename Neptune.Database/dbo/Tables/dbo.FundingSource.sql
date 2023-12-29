CREATE TABLE [dbo].[FundingSource](
	[FundingSourceID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FundingSource_FundingSourceID] PRIMARY KEY,
	[OrganizationID] [int] NOT NULL CONSTRAINT [FK_FundingSource_Organization_OrganizationID] FOREIGN KEY REFERENCES [dbo].[Organization] ([OrganizationID]),
	[FundingSourceName] [varchar](200),
	[IsActive] [bit] NOT NULL,
	[FundingSourceDescription] [varchar](500) NULL,
	CONSTRAINT [AK_FundingSource_OrganizationID_FundingSourceName] UNIQUE([OrganizationID], [FundingSourceName])
)