SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRUCharacteristic](
	[HRUCharacteristicID] [int] IDENTITY(1,1) NOT NULL,
	[HydrologicSoilGroup] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SlopePercentage] [int] NOT NULL,
	[ImperviousAcres] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[NetworkCatchmentID] [int] NULL,
	[Area] [float] NOT NULL,
	[HRUCharacteristicLandUseCodeID] [int] NOT NULL,
 CONSTRAINT [PK_HRUCharacteristic_HRUCharacteristicID] PRIMARY KEY CLUSTERED 
(
	[HRUCharacteristicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_HRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID] FOREIGN KEY([HRUCharacteristicLandUseCodeID])
REFERENCES [dbo].[HRUCharacteristicLandUseCode] ([HRUCharacteristicLandUseCodeID])
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [FK_HRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID]
GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_HRUCharacteristic_NetworkCatchment_NetworkCatchmentID] FOREIGN KEY([NetworkCatchmentID])
REFERENCES [dbo].[NetworkCatchment] ([NetworkCatchmentID])
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [FK_HRUCharacteristic_NetworkCatchment_NetworkCatchmentID]
GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_HRUCharacteristic_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [FK_HRUCharacteristic_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [FK_HRUCharacteristic_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [FK_HRUCharacteristic_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [CK_HRUCharacteristic_SlopePercentageIsAPercentage] CHECK  (([SlopePercentage]>=(0) AND [SlopePercentage]<=(100)))
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [CK_HRUCharacteristic_SlopePercentageIsAPercentage]
GO
ALTER TABLE [dbo].[HRUCharacteristic]  WITH CHECK ADD  CONSTRAINT [CK_HRUCharacteristic_XorForeignKeys] CHECK  (([TreatmentBMPID] IS NOT NULL AND [WaterQualityManagementPlanID] IS NULL AND [NetworkCatchmentID] IS NULL OR [TreatmentBMPID] IS NULL AND [WaterQualityManagementPlanID] IS NOT NULL AND [NetworkCatchmentID] IS NULL OR [TreatmentBMPID] IS NULL AND [WaterQualityManagementPlanID] IS NULL AND [NetworkCatchmentID] IS NOT NULL))
GO
ALTER TABLE [dbo].[HRUCharacteristic] CHECK CONSTRAINT [CK_HRUCharacteristic_XorForeignKeys]