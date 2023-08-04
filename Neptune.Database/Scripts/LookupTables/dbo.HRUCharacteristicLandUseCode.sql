MERGE INTO dbo.HRUCharacteristicLandUseCode AS Target
USING (VALUES
(1, 'COMM', 'Commercial'),
(2, 'EDU', 'Education'),
(3, 'IND', 'Industrial'),
(4, 'UTIL', 'Utility'),
(5, 'RESSFH', 'Residential - Single Family High Density'),
(6, 'RESSFL', 'Residential - Single Family Low Density'),
(7, 'RESMF', 'Residential - MultiFamily'),
(8, 'TRFWY', 'Transportation - Freeway'),
(9, 'TRANS', 'Transportation - Local Road'),
(10, 'TROTH', 'Transportation - Other'),
(11, 'OSAGIR', 'Open Space - Irrigated Agriculture'),
(12, 'OSAGNI', 'Open Space - Non-Irrigated Agriculture'),
(13, 'OSDEV', 'Open Space - Low Density Development'),
(14, 'OSIRR', 'Open Space - Irrigated Recreation'),
(15, 'OSLOW', 'Open Space - Low Canopy Vegetation'),
(16, 'OSFOR', 'Open Space - Forest'),
(17, 'OSWET', 'Open Space - Wetlands'),
(18, 'OSVAC', 'Open Space - Vacant Land'),
(19, 'WATER', 'Water')
)
AS Source (HRUCharacteristicLandUseCodeID, HRUCharacteristicLandUseCodeName, HRUCharacteristicLandUseCodeDisplayName)
ON Target.HRUCharacteristicLandUseCodeID = Source.HRUCharacteristicLandUseCodeID
WHEN MATCHED THEN
UPDATE SET
	HRUCharacteristicLandUseCodeName = Source.HRUCharacteristicLandUseCodeName,
	HRUCharacteristicLandUseCodeDisplayName = Source.HRUCharacteristicLandUseCodeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (HRUCharacteristicLandUseCodeID, HRUCharacteristicLandUseCodeName, HRUCharacteristicLandUseCodeDisplayName)
	VALUES (HRUCharacteristicLandUseCodeID, HRUCharacteristicLandUseCodeName, HRUCharacteristicLandUseCodeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;