DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '0001 - Add DataLength value to FileResource table'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    --update dbo.FileResource set ContentLength = DATALENGTH(FileResourceData)

	
    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Stewart Gordon', @MigrationName, '0001 - Add DataLength value to FileResource table'
END