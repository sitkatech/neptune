CREATE TABLE [dbo].[ProjectLoadGeneratingUnit](
	[ProjectLoadGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID] PRIMARY KEY,
	[ProjectLoadGeneratingUnitGeometry] [geometry] NOT NULL,
	[ProjectID] [int] NOT NULL CONSTRAINT [FK_ProjectLoadGeneratingUnit_Project_ProjectID] FOREIGN KEY REFERENCES [dbo].[Project] ([ProjectID]),
	[ModelBasinID] [int] NULL CONSTRAINT [FK_ProjectLoadGeneratingUnit_ModelBasin_ModelBasinID] FOREIGN KEY REFERENCES [dbo].[ModelBasin] ([ModelBasinID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_ProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_ProjectLoadGeneratingUnit_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_ProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[IsEmptyResponseFromHRUService] [bit] NULL,
)
GO

create spatial index [SPATIAL_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitGeometry] on [dbo].[ProjectLoadGeneratingUnit]
(
	[ProjectLoadGeneratingUnitGeometry]
)
with (BOUNDING_BOX=(1.8344e+006, 638677, 1.88829e+006, 692320))
