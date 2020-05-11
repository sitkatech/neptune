SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPModelingAttribute](
	[TreatmentBMPModelingAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[UpstreamTreatmentBMPID] [int] NULL,
	[AverageDivertedFlowrate] [float] NULL,
	[AverageTreatmentFlowrate] [float] NULL,
	[DesignDryWeatherTreatmentCapacity] [float] NULL,
	[DesignLowFlowDiversionCapacity] [float] NULL,
	[DesignMediaFiltrationRate] [float] NULL,
	[DesignResidenceTimeforPermanentPool] [float] NULL,
	[DiversionRate] [float] NULL,
	[DrawdownTimeforWQDetentionVolume] [float] NULL,
	[EffectiveFootprint] [float] NULL,
	[EffectiveRetentionDepth] [float] NULL,
	[InfiltrationDischargeRate] [float] NULL,
	[InfiltrationSurfaceArea] [float] NULL,
	[MediaBedFootprint] [float] NULL,
	[PermanentPoolorWetlandVolume] [float] NULL,
	[RoutingConfigurationID] [int] NULL,
	[StorageVolumeBelowLowestOutletElevation] [float] NULL,
	[SummerHarvestedWaterDemand] [float] NULL,
	[TimeOfConcentrationID] [int] NULL,
	[DrawdownTimeForDetentionVolume] [float] NULL,
	[TotalEffectiveBMPVolume] [float] NULL,
	[TotalEffectiveDrywellBMPVolume] [float] NULL,
	[TreatmentRate] [float] NULL,
	[UnderlyingHydrologicSoilGroupID] [int] NULL,
	[UnderlyingInfiltrationRate] [float] NULL,
	[WaterQualityDetentionVolume] [float] NULL,
	[WettedFootprint] [float] NULL,
	[WinterHarvestedWaterDemand] [float] NULL,
	[OperationMonthID] [int] NULL,
 CONSTRAINT [PK_TreatmentBMPModelingAttribute_TreatmentBMPModelingAttributeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPModelingAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPModelingAttribute_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK__TreatmentBMPModelingAttribute_OperationMonth_OperationMonthID] FOREIGN KEY([OperationMonthID])
REFERENCES [dbo].[OperationMonth] ([OperationMonthID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK__TreatmentBMPModelingAttribute_OperationMonth_OperationMonthID]
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPModelingAttribute_RoutingConfiguration_RoutingConfigurationID] FOREIGN KEY([RoutingConfigurationID])
REFERENCES [dbo].[RoutingConfiguration] ([RoutingConfigurationID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK_TreatmentBMPModelingAttribute_RoutingConfiguration_RoutingConfigurationID]
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPModelingAttribute_TimeOfConcentration_TimeOfConcentrationID] FOREIGN KEY([TimeOfConcentrationID])
REFERENCES [dbo].[TimeOfConcentration] ([TimeOfConcentrationID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK_TreatmentBMPModelingAttribute_TimeOfConcentration_TimeOfConcentrationID]
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPModelingAttribute_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK_TreatmentBMPModelingAttribute_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPModelingAttribute_TreatmentBMP_UpstreamTreatmentBMPID_TreatmentBMPID] FOREIGN KEY([UpstreamTreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK_TreatmentBMPModelingAttribute_TreatmentBMP_UpstreamTreatmentBMPID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPModelingAttribute_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID] FOREIGN KEY([UnderlyingHydrologicSoilGroupID])
REFERENCES [dbo].[UnderlyingHydrologicSoilGroup] ([UnderlyingHydrologicSoilGroupID])
GO
ALTER TABLE [dbo].[TreatmentBMPModelingAttribute] CHECK CONSTRAINT [FK_TreatmentBMPModelingAttribute_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID]