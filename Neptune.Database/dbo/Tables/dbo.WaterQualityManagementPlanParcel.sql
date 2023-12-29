CREATE TABLE [dbo].[WaterQualityManagementPlanParcel](
	[WaterQualityManagementPlanParcelID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanParcelID] PRIMARY KEY,
	[WaterQualityManagementPlanID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[ParcelID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanParcel_Parcel_ParcelID] FOREIGN KEY REFERENCES [dbo].[Parcel] ([ParcelID]),
	CONSTRAINT [AK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanID_ParcelID] UNIQUE ([WaterQualityManagementPlanID], [ParcelID])
)