CREATE TABLE [dbo].[OrganizationType](
	[OrganizationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationTypeName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OrganizationTypeAbbreviation] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LegendColor] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDefaultOrganizationType] [bit] NOT NULL,
 CONSTRAINT [PK_OrganizationType_OrganizationTypeID] PRIMARY KEY CLUSTERED 
(
	[OrganizationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
