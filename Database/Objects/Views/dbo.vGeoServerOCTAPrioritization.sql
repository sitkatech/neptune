Drop View If Exists dbo.vGeoServerOCTAPrioritization
Go

Create View dbo.vGeoServerOCTAPrioritization As
Select
	o.OCTAPrioritizationID,
	o.OCTAPrioritizationKey,
	o.OCTAPrioritizationGeometry4326 as OCTAPrioritizationGeometry,
	o.TPI as TransportationNexusScore,
	o.WQNLU as LandUseBasedWaterQualityNeedScore,
	o.WQNMON as ReceivingWaterScore,
	o.SEA as StrategicallyEffectiveAreaScore
From dbo.OCTAPrioritization o
GO