SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterJurisdictionPerson](
	[StormwaterJurisdictionPersonID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
 CONSTRAINT [PK_StormwaterJurisdictionPerson_StormwaterJurisdictionPersonID] PRIMARY KEY CLUSTERED 
(
	[StormwaterJurisdictionPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_StormaterJurisdictionPerson_StormwaterJurisdictionPersonID_TenantID] UNIQUE NONCLUSTERED 
(
	[StormwaterJurisdictionPersonID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionPerson_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson] CHECK CONSTRAINT [FK_StormwaterJurisdictionPerson_Person_PersonID]
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionPerson_Person_PersonID_TenantID] FOREIGN KEY([PersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson] CHECK CONSTRAINT [FK_StormwaterJurisdictionPerson_Person_PersonID_TenantID]
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson] CHECK CONSTRAINT [FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID] FOREIGN KEY([StormwaterJurisdictionID], [TenantID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID], [TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson] CHECK CONSTRAINT [FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID_TenantID]
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson]  WITH CHECK ADD  CONSTRAINT [FK_StormwaterJurisdictionPerson_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[StormwaterJurisdictionPerson] CHECK CONSTRAINT [FK_StormwaterJurisdictionPerson_Tenant_TenantID]