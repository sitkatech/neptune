DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '011 - add neptune page for wqmp annual report'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(89, ''),
		(90, '')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Shannon Bulloch', @MigrationName, '011 - add neptune page for wqmp annual report'
END