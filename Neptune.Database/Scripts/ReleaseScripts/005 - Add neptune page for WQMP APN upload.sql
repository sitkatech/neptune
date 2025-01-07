DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '005 - Add neptune page for WQMP APN upload'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(71, 'WQMP Boundary from APN List')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Shannon Bulloch', @MigrationName, '005 - Add neptune page for WQMP APN upload'
END