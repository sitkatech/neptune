MERGE INTO dbo.StormwaterBreadCrumbEntity AS Target
USING (VALUES
(1, N'TreatmentBMP', N'Treatment BMP', 'glyphicon-leaf', N'treatmentBMPColor'),
(3, N'Jurisdiction', N'Jurisdiction', 'glyphicon-home', N'jurisdictionColor'),
(4, N'Users', N'Users', 'glyphicon-user', N'userColor'),
(5, N'Assessments', N'Assessments', 'glyphicon-pencil', N'registrationColor'),
(6, N'FieldVisits', N'Field Visits', 'glyphicon-globe', N'registrationColor'),
(7, N'FieldRecords', N'Field Records', 'glyphicon-globe', N'registrationColor'),
(8, N'WaterQualityManagementPlan', N'Water Quality Management Plan', 'glyphicon-list-alt', N'waterQualityManagementPlanColor'),
(9, N'Parcel', N'Parcel', 'glyphicon-home', N'parcelColor'),
(10, 'OnlandVisualTrashAssessment', 'Onland Visual Trash Assessment', 'glyphicon-trash', 'onlandVisualTrashAssessmentColor')
)
AS Source (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass)
ON Target.StormwaterBreadCrumbEntityID = Source.StormwaterBreadCrumbEntityID
WHEN MATCHED THEN
UPDATE SET
	StormwaterBreadCrumbEntityName = Source.StormwaterBreadCrumbEntityName,
	StormwaterBreadCrumbEntityDisplayName = Source.StormwaterBreadCrumbEntityDisplayName,
	GlyphIconClass = Source.GlyphIconClass,
	ColorClass = Source.ColorClass
WHEN NOT MATCHED BY TARGET THEN
	INSERT (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass)
	VALUES (StormwaterBreadCrumbEntityID, StormwaterBreadCrumbEntityName, StormwaterBreadCrumbEntityDisplayName, GlyphIconClass, ColorClass)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;