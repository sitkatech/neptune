DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '008 - Add neptune page for SPA home page'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
	values
		(87, '<h1 style="text-align: center;">About the OC Stormwater Tools</h1> <p>The <strong>OC Stormwater Tools</strong> platform was developed as a collaborative effort of Orange County Public Works, MS4 Permittees, and other organizations within the South Orange County Watershed Management Area. The platform consists of three modules, plus workflows to support delineations.&nbsp;</p>')

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Jamie Quishenberry', @MigrationName, 'Add neptune page for SPA home page'
END