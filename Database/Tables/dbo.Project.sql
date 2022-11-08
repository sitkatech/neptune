SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[ProjectStatusID] [int] NOT NULL,
	[PrimaryContactPersonID] [int] NOT NULL,
	[CreatePersonID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ProjectDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdditionalContactInformation] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoesNotIncludeTreatmentBMPs] [bit] NOT NULL,
	[CalculateOCTAM2Tier2Scores] [bit] NOT NULL,
	[ShareOCTAM2Tier2Scores] [bit] NOT NULL,
	[OCTAM2Tier2ScoresLastSharedDate] [datetime] NULL,
	[OCTAWatersheds] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
 CONSTRAINT [PK_Project_ProjectID] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Project_ProjectName] UNIQUE NONCLUSTERED 
(
	[ProjectName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Person_CreatePersonID_PersonID] FOREIGN KEY([CreatePersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Person_CreatePersonID_PersonID]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Person_PrimaryContactPersonID_PersonID] FOREIGN KEY([PrimaryContactPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Person_PrimaryContactPersonID_PersonID]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_ProjectStatus_ProjectStatusID] FOREIGN KEY([ProjectStatusID])
REFERENCES [dbo].[ProjectStatus] ([ProjectStatusID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_ProjectStatus_ProjectStatusID]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_StormwaterJurisdiction_StormwaterJurisdictionID]