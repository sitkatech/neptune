SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlannedProjectNereidResult](
	[PlannedProjectNereidResultID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[IsBaselineCondition] [bit] NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[NodeID] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FullResponse] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_PlannedProjectNereidResult_PlannedProjectNereidResultID] PRIMARY KEY CLUSTERED 
(
	[PlannedProjectNereidResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PlannedProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectNereidResult_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[PlannedProjectNereidResult] CHECK CONSTRAINT [FK_PlannedProjectNereidResult_Project_ProjectID]