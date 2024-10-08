MERGE INTO dbo.HydromodificationAppliesType AS Target
USING (VALUES
(1, 'Applicable ', 'Applicable'),
(2, 'Exempt', 'Exempt')
)
AS Source (HydromodificationAppliesTypeID, HydromodificationAppliesTypeName, HydromodificationAppliesTypeDisplayName)
ON Target.HydromodificationAppliesTypeID = Source.HydromodificationAppliesTypeID
WHEN MATCHED THEN
UPDATE SET
	HydromodificationAppliesTypeName = Source.HydromodificationAppliesTypeName,
	HydromodificationAppliesTypeDisplayName = Source.HydromodificationAppliesTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (HydromodificationAppliesTypeID, HydromodificationAppliesTypeName, HydromodificationAppliesTypeDisplayName)
	VALUES (HydromodificationAppliesTypeID, HydromodificationAppliesTypeName, HydromodificationAppliesTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;