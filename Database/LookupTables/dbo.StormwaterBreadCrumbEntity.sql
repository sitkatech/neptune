delete from dbo.StormwaterBreadCrumbEntity
go

INSERT 
dbo.StormwaterBreadCrumbEntity (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass) VALUES
 (1, N'TreatmentBMP', N'Treatment BMP', 'glyphicon-leaf', N'treatmentBMPColor'),
 (2, N'ModeledCatchment', N'Modeled Catchment', 'glyphicon-tint', N'modeledCatchmentColor'),
 (3, N'Jurisdiction', N'Jurisdiction', 'glyphicon-home', N'jurisdictionColor'),
 (4, N'Users', N'Users', 'glyphicon-user', N'userColor'),
 (5, N'Assessments', N'Assessments', 'glyphicon-pencil', N'registrationColor')