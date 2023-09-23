MERGE INTO dbo.SourceControlBMPAttributeCategory AS Target
USING (VALUES
(1, 'Site Design', 'Hydrologic Source Control and Site Design BMPs'),
(2, 'Non-Structural', 'Applicable Routine Non-Structural Source Control BMPs'),
(3, 'Structural', 'Applicable Routine Structural Source Control BMPs')
)
AS Source (SourceControlBMPAttributeCategoryID, SourceControlBMPAttributeCategoryShortName, SourceControlBMPAttributeCategoryName)
ON Target.SourceControlBMPAttributeCategoryID = Source.SourceControlBMPAttributeCategoryID
WHEN MATCHED THEN
UPDATE SET
	SourceControlBMPAttributeCategoryName = Source.SourceControlBMPAttributeCategoryName,
	SourceControlBMPAttributeCategoryShortName = Source.SourceControlBMPAttributeCategoryShortName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (SourceControlBMPAttributeCategoryID, SourceControlBMPAttributeCategoryShortName, SourceControlBMPAttributeCategoryName)
	VALUES (SourceControlBMPAttributeCategoryID, SourceControlBMPAttributeCategoryShortName, SourceControlBMPAttributeCategoryName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;