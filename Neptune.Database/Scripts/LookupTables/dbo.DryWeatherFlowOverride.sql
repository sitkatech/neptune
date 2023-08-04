MERGE INTO dbo.DryWeatherFlowOverride AS Target
USING (VALUES
(1, 'No', 'No - As Modeled'),
(2, 'Yes', 'Yes - DWF Effectively Eliminated')
)
AS Source (DryWeatherFlowOverrideID, DryWeatherFlowOverrideName, DryWeatherFlowOverrideDisplayName)
ON Target.DryWeatherFlowOverrideID = Source.DryWeatherFlowOverrideID
WHEN MATCHED THEN
UPDATE SET
	DryWeatherFlowOverrideName = Source.DryWeatherFlowOverrideName,
	DryWeatherFlowOverrideDisplayName = Source.DryWeatherFlowOverrideDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (DryWeatherFlowOverrideID, DryWeatherFlowOverrideName, DryWeatherFlowOverrideDisplayName)
	VALUES (DryWeatherFlowOverrideID, DryWeatherFlowOverrideName, DryWeatherFlowOverrideDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;