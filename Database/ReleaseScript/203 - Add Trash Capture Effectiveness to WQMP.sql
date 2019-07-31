Alter Table dbo.WaterQualityManagementPlan
Add TrashCaptureEffectiveness int null
go

ALTER TABLE [dbo].WaterQualityManagementPlan  WITH CHECK ADD  CONSTRAINT [CK_WaterQualityManagementPlan_TrashCaptureEffectivenessMustBeBetween1And99] CHECK 
(([TrashCaptureEffectiveness] IS NULL OR [TrashCaptureEffectiveness]>(0) AND [TrashCaptureEffectiveness]<(100)))
GO