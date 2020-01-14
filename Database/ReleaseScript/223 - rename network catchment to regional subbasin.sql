ALTER TABLE dbo.HRUCharacteristic DROP CONSTRAINT CK_HRUCharacteristic_XorForeignKeys
DROP INDEX SPATIAL_NetworkCatchment_CatchmentGeometry ON dbo.NetworkCatchment
GO


exec sp_rename 'dbo.AK_NetworkCatchment_OCSurveyCatchmentID', 'AK_RegionalSubbasin_OCSurveyCatchmentID', 'OBJECT';
exec sp_rename 'dbo.FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID', 'FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID', 'OBJECT';
exec sp_rename 'dbo.FK_HRUCharacteristic_NetworkCatchment_NetworkCatchmentID', 'FK_HRUCharacteristic_RegionalSubbasin_RegionalSubbasinID', 'OBJECT';
exec sp_rename 'dbo.PK_NetworkCatchmentStaging_NetworkCatchmentStagingID', 'PK_RegionalSubbasinStaging_RegionalSubbasinStagingID', 'OBJECT';
exec sp_rename 'dbo.PK_NetworkCatchment_NetworkCatchmentID', 'PK_RegionalSubbasin_RegionalSubbasinID', 'OBJECT';
exec sp_rename N'dbo.NetworkCatchment.IX_NetworkCatchment_OCSurveyDownstreamCatchmentID', N'IX_RegionalSubbasin_OCSurveyDownstreamCatchmentID', N'INDEX';
exec sp_rename 'dbo.HRUCharacteristic.NetworkCatchmentID', 'RegionalSubbasinID', 'COLUMN';
exec sp_rename 'dbo.NetworkCatchmentStaging.NetworkCatchmentStagingID', 'RegionalSubbasinStagingID', 'COLUMN';
exec sp_rename 'dbo.NetworkCatchment.NetworkCatchmentID', 'RegionalSubbasinID', 'COLUMN';
exec sp_rename 'dbo.NetworkCatchmentStaging', 'RegionalSubbasinStaging';
exec sp_rename 'dbo.NetworkCatchment', 'RegionalSubbasin';
GO

--alter table dbo.NetworkCatchment add constraint PK_RegionalSubbasin_RegionalSubbasinID primary key (RegionalSubbasinID)

ALTER TABLE dbo.HRUCharacteristic  WITH CHECK ADD  CONSTRAINT CK_HRUCharacteristic_XorForeignKeys CHECK  ((TreatmentBMPID IS NOT NULL AND WaterQualityManagementPlanID IS NULL AND RegionalSubbasinID IS NULL OR TreatmentBMPID IS NULL AND WaterQualityManagementPlanID IS NOT NULL AND RegionalSubbasinID IS NULL OR TreatmentBMPID IS NULL AND WaterQualityManagementPlanID IS NULL AND RegionalSubbasinID IS NOT NULL))

CREATE SPATIAL INDEX SPATIAL_RegionalSubbasin_CatchmentGeometry ON dbo.RegionalSubbasin
(
	CatchmentGeometry
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8)
GO



update dbo.geometry_columns
set f_table_name = 'RegionalSubbasinStaging'
where f_table_name = 'NetworkCatchmentStaging'

update dbo.geometry_columns
set f_table_name = 'RegionalSubbasin'
where f_table_name = 'NetworkCatchment'