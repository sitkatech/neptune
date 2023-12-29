MERGE INTO dbo.FundingEventType AS Target
USING (VALUES
(1, 'PlanningAndDesign', 'Planning & Design'),
(2, 'CapitalConstruction', 'Capital Construction'),
(3, 'RoutineMaintenance', 'Routine Assessment and Maintenance'),
(4, 'RehabilitativeMaintenance', 'Rehabilitative Maintenance'),
(5, 'Retrofit', 'Retrofit')
)
AS Source (FundingEventTypeID, FundingEventTypeName, FundingEventTypeDisplayName)
ON Target.FundingEventTypeID = Source.FundingEventTypeID
WHEN MATCHED THEN
UPDATE SET
	FundingEventTypeName = Source.FundingEventTypeName,
	FundingEventTypeDisplayName = Source.FundingEventTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (FundingEventTypeID, FundingEventTypeName, FundingEventTypeDisplayName)
	VALUES (FundingEventTypeID, FundingEventTypeName, FundingEventTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;