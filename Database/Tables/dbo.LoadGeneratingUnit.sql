SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoadGeneratingUnit](
	[LoadGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL,
	[LoadGeneratingUnitGeometry] [geometry] NOT NULL,
	[LSPCBasinID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[IsEmptyResponseFromHRUService] [bit] NULL,
 CONSTRAINT [PK_LoadGeneratingUnit_LoadGeneratingUnitID] PRIMARY KEY CLUSTERED 
(
	[LoadGeneratingUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[LoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_LoadGeneratingUnit_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[LoadGeneratingUnit] CHECK CONSTRAINT [FK_LoadGeneratingUnit_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[LoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_LoadGeneratingUnit_LSPCBasin_LSPCBasinID] FOREIGN KEY([LSPCBasinID])
REFERENCES [dbo].[LSPCBasin] ([LSPCBasinID])
GO
ALTER TABLE [dbo].[LoadGeneratingUnit] CHECK CONSTRAINT [FK_LoadGeneratingUnit_LSPCBasin_LSPCBasinID]
GO
ALTER TABLE [dbo].[LoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_LoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY([RegionalSubbasinID])
REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID])
GO
ALTER TABLE [dbo].[LoadGeneratingUnit] CHECK CONSTRAINT [FK_LoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID]
GO
ALTER TABLE [dbo].[LoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_LoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[LoadGeneratingUnit] CHECK CONSTRAINT [FK_LoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID]