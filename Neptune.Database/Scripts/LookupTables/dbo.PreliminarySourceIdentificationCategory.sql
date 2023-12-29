MERGE INTO dbo.PreliminarySourceIdentificationCategory AS Target
USING (VALUES
(1, 'Vehicles', 'Vehicles'),
(2, 'InadequateWasteContainerManagement', 'Inadequate Waste Container Management'),
(3, 'PedestrianLitter', 'Pedestrian Litter'),
(4, 'IllegalDumping', 'Illegal Dumping')
)
AS Source (PreliminarySourceIdentificationCategoryID, PreliminarySourceIdentificationCategoryName, PreliminarySourceIdentificationCategoryDisplayName)
ON Target.PreliminarySourceIdentificationCategoryID = Source.PreliminarySourceIdentificationCategoryID
WHEN MATCHED THEN
UPDATE SET
	PreliminarySourceIdentificationCategoryName = Source.PreliminarySourceIdentificationCategoryName,
	PreliminarySourceIdentificationCategoryDisplayName = Source.PreliminarySourceIdentificationCategoryDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (PreliminarySourceIdentificationCategoryID, PreliminarySourceIdentificationCategoryName, PreliminarySourceIdentificationCategoryDisplayName)
	VALUES (PreliminarySourceIdentificationCategoryID, PreliminarySourceIdentificationCategoryName, PreliminarySourceIdentificationCategoryDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;