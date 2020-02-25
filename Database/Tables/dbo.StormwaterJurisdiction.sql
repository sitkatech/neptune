SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdiction](
	[StormwaterJurisdictionID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[StateProvinceID] [int] NOT NULL,
 CONSTRAINT [PK_StormwaterJurisdiction_StormwaterJurisdictionID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormwaterJurisdiction_OrganizationID] UNIQUE NONCLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[StormwaterJurisdiction]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID] FOREIGN KEY([StateProvinceID])
REFERENCES [dbo].[StateProvince] ([StateProvinceID])
GO
ALTER TABLE [dbo].[StormwaterJurisdiction] CHECK CONSTRAINT [FK_StormwaterJurisdiction_StateProvince_StateProvinceID]