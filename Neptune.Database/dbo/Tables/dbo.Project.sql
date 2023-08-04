CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Project_ProjectID] PRIMARY KEY,
	[ProjectName] [varchar](200) CONSTRAINT [AK_Project_ProjectName] UNIQUE,
	[OrganizationID] [int] NOT NULL CONSTRAINT [FK_Project_Organization_OrganizationID] FOREIGN KEY REFERENCES [dbo].[Organization] ([OrganizationID]),
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_Project_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[ProjectStatusID] [int] NOT NULL CONSTRAINT [FK_Project_ProjectStatus_ProjectStatusID] FOREIGN KEY REFERENCES [dbo].[ProjectStatus] ([ProjectStatusID]),
	[PrimaryContactPersonID] [int] NOT NULL CONSTRAINT [FK_Project_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[CreatePersonID] [int] NOT NULL CONSTRAINT [FK_Project_Person_CreatePersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[DateCreated] [datetime] NOT NULL,
	[ProjectDescription] [varchar](500) NULL,
	[AdditionalContactInformation] [varchar](500) NULL,
	[DoesNotIncludeTreatmentBMPs] [bit] NOT NULL,
	[CalculateOCTAM2Tier2Scores] [bit] NOT NULL,
	[ShareOCTAM2Tier2Scores] [bit] NOT NULL,
	[OCTAM2Tier2ScoresLastSharedDate] [datetime] NULL,
	[OCTAWatersheds] [varchar](500) NULL,
	[PollutantVolume] [float] NULL,
	[PollutantMetals] [float] NULL,
	[PollutantBacteria] [float] NULL,
	[PollutantNutrients] [float] NULL,
	[PollutantTSS] [float] NULL,
	[TPI] [float] NULL,
	[SEA] [float] NULL,
	[DryWeatherWQLRI] [float] NULL,
	[WetWeatherWQLRI] [float] NULL,
	[AreaTreatedAcres] [float] NULL,
	[ImperviousAreaTreatedAcres] [float] NULL,
	[UpdatePersonID] [int] NULL CONSTRAINT [FK_Project_Person_UpdatePersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[DateUpdated] [datetime] NULL
)