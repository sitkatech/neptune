CREATE TABLE [dbo].[OrganizationType](
	[OrganizationTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OrganizationType_OrganizationTypeID] PRIMARY KEY,
	[OrganizationTypeName] [varchar](200),
	[OrganizationTypeAbbreviation] [varchar](100),
	[LegendColor] [varchar](10),
	[IsDefaultOrganizationType] [bit] NOT NULL
)
