DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '015 - Add Field Definition for DownstreamOfNonModeledBMP'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.FieldDefinition(FieldDefinitionTypeID, FieldDefinitionValue)
	values
		(147, '')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '015 - Add Field Definition for DownstreamOfNonModeledBMP'
END