SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DirtyModelNode](
	[DirtyModelNodeID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[DelineationID] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DirtyModelNode_DirtyModelNodeID] PRIMARY KEY CLUSTERED 
(
	[DirtyModelNodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
