CREATE TABLE [dbo].[Organization](
	[OrganizationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Organization_OrganizationID] PRIMARY KEY,
	[OrganizationGuid] [uniqueidentifier] NULL,
	[OrganizationName] [varchar](200),
	[OrganizationShortName] [varchar](50) NULL,
	[PrimaryContactPersonID] [int] NULL CONSTRAINT [FK_Organization_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[IsActive] [bit] NOT NULL,
	[OrganizationUrl] [varchar](200) NULL,
	[LogoFileResourceID] [int] NULL CONSTRAINT [FK_Organization_FileResource_LogoFileResourceID_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[OrganizationTypeID] [int] NOT NULL CONSTRAINT [FK_Organization_OrganizationType_OrganizationTypeID] FOREIGN KEY REFERENCES [dbo].[OrganizationType] ([OrganizationTypeID])
)