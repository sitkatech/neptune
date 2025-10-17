DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '019 - Add Dry Weather Flow Override custom attribute for all Modeled Treatment BMPs without it'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
	
	PRINT @MigrationName;

    --Because DryWeatherFlowOverride was null for some BMPs, but it's now a requirement for all BMPs, we need to add the CustomAttribute for all TreatmentBMPs that don't already have it.
    --Otherwise the project workflow claims there are unsaved changes when you open a project with those BMPs.

    declare @currentCustomAttributeIDMax int = (select max(CustomAttributeID) from dbo.CustomAttribute)

    insert into dbo.CustomAttribute (TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, TreatmentBMPTypeID, CustomAttributeTypeID)
    select TreatmentBMPID, TreatmentBMPTypeCustomAttributeTypeID, tbmp.TreatmentBMPTypeID, cat.CustomAttributeTypeID
    from dbo.TreatmentBMP tbmp
             join dbo.TreatmentBMPTypeCustomAttributeType tbmptcat on tbmp.TreatmentBMPTypeID = tbmptcat.TreatmentBMPTypeID
             join dbo.CustomAttributeType cat on tbmptcat.CustomAttributeTypeID = cat.CustomAttributeTypeID
    where cat.CustomAttributeTypeName = 'Dry Weather Flow Override' and TreatmentBMPID in (
        SELECT DISTINCT TreatmentBMPID
        FROM dbo.CustomAttribute ca1
        WHERE NOT EXISTS (
            SELECT 1
            FROM dbo.CustomAttribute ca2
            join dbo.CustomAttributeType cat on ca2.CustomAttributeTypeID = cat.CustomAttributeTypeID
            WHERE ca2.TreatmentBMPID = ca1.TreatmentBMPID
              AND cat.CustomAttributeTypeName = 'Dry Weather Flow Override'
        ))

        insert into dbo.CustomAttributeValue (CustomAttributeID, AttributeValue)
    select CustomAttributeID, DryWeatherFlowOverrideDisplayName
    from dbo.CustomAttribute ca
             cross join dbo.DryWeatherFlowOverride dwfo
    where ca.CustomAttributeID > @currentCustomAttributeIDMax and dwfo.DryWeatherFlowOverrideID = 1

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'Mack Peters', @MigrationName, '019 - Add Dry Weather Flow Override custom attribute for all Modeled Treatment BMPs without it'
END