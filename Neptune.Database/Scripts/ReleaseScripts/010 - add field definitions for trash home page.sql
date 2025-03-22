DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '010 - add field definitions for trash home page'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.FieldDefinition(FieldDefinitionTypeID, FieldDefinitionValue)
	values
		(142, ''),
		(143, ''),
		(144, ''),
		(145, ''),
		(146, '')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Shannon Bulloch', @MigrationName, '010 - add field definitions for trash home page'
END