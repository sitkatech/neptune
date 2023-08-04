MERGE INTO dbo.RoutingConfiguration AS Target
USING (VALUES
(1, 'Online', 'Online'),
(2, 'Offline', 'Offline')
)
AS Source (RoutingConfigurationID, RoutingConfigurationName, RoutingConfigurationDisplayName)
ON Target.RoutingConfigurationID = Source.RoutingConfigurationID
WHEN MATCHED THEN
UPDATE SET
	RoutingConfigurationName = Source.RoutingConfigurationName,
	RoutingConfigurationDisplayName = Source.RoutingConfigurationDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (RoutingConfigurationID, RoutingConfigurationName, RoutingConfigurationDisplayName)
	VALUES (RoutingConfigurationID, RoutingConfigurationName, RoutingConfigurationDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;