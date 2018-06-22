delete from dbo.StormwaterBreadCrumbEntity
go

INSERT 
dbo.StormwaterBreadCrumbEntity (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass) VALUES
 (1, N'TreatmentBMP', N'Treatment BMP', 'glyphicon-leaf', N'treatmentBMPColor'),
 (2, N'ModeledCatchment', N'Modeled Catchment', 'glyphicon-tint', N'modeledCatchmentColor'),
 (3, N'Jurisdiction', N'Jurisdiction', 'glyphicon-home', N'jurisdictionColor'),
 (4, N'Users', N'Users', 'glyphicon-user', N'userColor'),
 (5, N'Assessments', N'Assessments', 'glyphicon-pencil', N'registrationColor'),
 (6, N'FieldVisits', N'Field Visits', 'glyphicon-globe', N'registrationColor'),
 (7, N'FieldRecords', N'Field Records', 'glyphicon-globe', N'registrationColor'),
 (8, N'WaterQualityManagementPlan', N'Water Quality Management Plan', 'glyphicon-list-alt', N'waterQualityManagementPlanColor'),
 (9, N'Parcel', N'Parcel', 'glyphicon-home', N'parcelColor')
