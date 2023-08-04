Drop View If Exists dbo.vGeoServerOCTAPrioritization
Go

Create View dbo.vGeoServerOCTAPrioritization As
Select
	o.OCTAPrioritizationID,
	o.OCTAPrioritizationKey,
	o.OCTAPrioritizationGeometry4326 as OCTAPrioritizationGeometry,
	o.CatchIDN,
	o.Watershed,
	o.TPI as TransportationNexusScore,
	o.WQNLU as LandUseBasedWaterQualityNeedScore,
	o.WQNMON as ReceivingWaterScore,
	o.SEA as StrategicallyEffectiveAreaScore,
	o.PC_BAC_PCT,
	o.PC_MET_PCT,
	o.PC_NUT_PCT,
	o.PC_TSS_PCT,
	o.PC_VOL_PCT
From dbo.OCTAPrioritization o
GO