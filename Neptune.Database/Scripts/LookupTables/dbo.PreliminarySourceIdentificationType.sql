MERGE INTO dbo.PreliminarySourceIdentificationType AS Target
USING (VALUES
(1,'Moving Vehicles', 'MovingVehicles', 1),
(2,'Parked Cars', 'ParkedCars', 1),
(3,'Uncovered Loads', 'UncoveredLoads', 1),
(4,'Vehicles (Other)', 'VehiclesOther', 1),
(5,'Overflowing or uncovered receptacles/dumpsters', 'OverflowingReceptacles', 2),
(6,'Dispersal of household trash and recyclables around the collection process', 'TrashDispersal', 2),
(7,'Inadequate Waste Container Management (Other)', 'InadequateWasteContainerManagementOther', 2),
(8,'Restaurants', 'Restaurants', 3),
(9,'Convenience Stores', 'ConvenienceStores', 3),
(10,'Liquor Stores', 'LiquorStores', 3),
(11,'Bus Stops', 'BusStops', 3),
(12,'Special Events', 'SpecialEvents', 3),
(13,'Pedestrian Litter (Other)', 'PedestrianLitterOther', 3),
(14,'Illegal dumping on-land', 'IllegalDumpingOnLand', 4),
(15,'Homeless encampments', 'Homelessencampments', 4),
(16,'Illegal Dumping (Other)', 'IllegalDumpingOther', 4)
)
AS Source (PreliminarySourceIdentificationTypeID, PreliminarySourceIdentificationTypeDisplayName, PreliminarySourceIdentificationTypeName, PreliminarySourceIdentificationCategoryID)
ON Target.PreliminarySourceIdentificationTypeID = Source.PreliminarySourceIdentificationTypeID
WHEN MATCHED THEN
UPDATE SET
	PreliminarySourceIdentificationTypeName = Source.PreliminarySourceIdentificationTypeName,
	PreliminarySourceIdentificationTypeDisplayName = Source.PreliminarySourceIdentificationTypeDisplayName,
	PreliminarySourceIdentificationCategoryID = Source.PreliminarySourceIdentificationCategoryID
WHEN NOT MATCHED BY TARGET THEN
	INSERT (PreliminarySourceIdentificationTypeID, PreliminarySourceIdentificationTypeDisplayName, PreliminarySourceIdentificationTypeName, PreliminarySourceIdentificationCategoryID)
	VALUES (PreliminarySourceIdentificationTypeID, PreliminarySourceIdentificationTypeDisplayName, PreliminarySourceIdentificationTypeName, PreliminarySourceIdentificationCategoryID)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;