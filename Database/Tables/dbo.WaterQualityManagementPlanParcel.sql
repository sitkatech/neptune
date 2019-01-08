SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanParcel](
	[WaterQualityManagementPlanParcelID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[ParcelID] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanParcelID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanParcelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanID_ParcelID] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanID] ASC,
	[ParcelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanParcel]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanParcel_Parcel_ParcelID] FOREIGN KEY([ParcelID])
REFERENCES [dbo].[Parcel] ([ParcelID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanParcel] CHECK CONSTRAINT [FK_WaterQualityManagementPlanParcel_Parcel_ParcelID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanParcel]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanParcel] CHECK CONSTRAINT [FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID]