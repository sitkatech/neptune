DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '003- Add neptune page for upload simplified bmps'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(69, 'Simplified BMPs')
    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Liz Arikawa', @MigrationName, '003- Add neptune page for upload simplified bmps'
END