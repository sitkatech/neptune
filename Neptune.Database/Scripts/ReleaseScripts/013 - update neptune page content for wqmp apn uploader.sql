DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '013 - update neptune page content for wqmp apn uploader'
IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    update dbo.NeptunePage
    set NeptunePageContent = '<p class="MsoNormal">Use this form to bulk upload or bulk update WQMPs boundary polygon in the system from a comma separated list of APNs. The WQMP Name must match an existing record in the database. If an APN provided in the csv file doesn''t match a record in the database the upload will proceed and with records that found a match.</p><p class="MsoNormal">The CSV must have a header row with columns for "WQMP", "APNs" and "WQMP Boundary Notes".</p><p class="MsoNormal">If the upload fails a detailed error report will be provided, otherwise WQMPs boundary will be created per the CSV. Once uploaded, this action cannot be undone. <strong>It is strongly advised to test the upload in the QA site (</strong><a title="https://qa.ocstormwatertools.org" href="https://qa.ocstormwatertools.org/"><strong>https://qa.ocstormwatertools.org</strong></a><strong>) prior to bulk uploading data to the Production site.</strong></p>'
    where NeptunePageTypeID = 71

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Liz Arikawa', @MigrationName, '013 - update neptune page content for wqmp apn uploader'
END