MERGE INTO dbo.ProjectStatus AS Target
USING (VALUES
(1, 'Draft', 'Draft')
)
AS Source (ProjectStatusID, ProjectStatusName, ProjectStatusDisplayName)
ON Target.ProjectStatusID = Source.ProjectStatusID
WHEN MATCHED THEN
UPDATE SET
	ProjectStatusName = Source.ProjectStatusName,
	ProjectStatusDisplayName = Source.ProjectStatusDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ProjectStatusID, ProjectStatusName, ProjectStatusDisplayName)
	VALUES (ProjectStatusID, ProjectStatusName, ProjectStatusDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;