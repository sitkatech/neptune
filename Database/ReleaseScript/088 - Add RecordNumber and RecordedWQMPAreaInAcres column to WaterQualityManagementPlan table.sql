ALTER TABLE dbo.WaterQualityManagementPlan
ADD
RecordNumber VARCHAR(500) NULL, 
RecordedWQMPAreaInAcres DECIMAL(5,1) NULL;


ALTER TABLE dbo.Parcel
ADD ParcelAreaInAcres float;
GO

UPDATE dbo.Parcel SET ParcelAreaInAcres = ParcelGeometry.STArea() * 0.0002471053821147119


