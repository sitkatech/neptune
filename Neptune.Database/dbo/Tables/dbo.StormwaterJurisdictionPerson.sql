CREATE TABLE [dbo].[StormwaterJurisdictionPerson](
	[StormwaterJurisdictionPersonID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_StormwaterJurisdictionPerson_StormwaterJurisdictionPersonID] PRIMARY KEY,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_StormwaterJurisdictionPerson_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[PersonID] [int] NOT NULL CONSTRAINT [FK_StormwaterJurisdictionPerson_Person_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID])
)