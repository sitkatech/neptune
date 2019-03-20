Create table dbo.PreliminarySourceIdentificationCategory(
PreliminarySourceIdentificationCategoryID int not null constraint PK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID primary key,
PreliminarySourceIdentificationCategoryName varchar(100) not null constraint AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryName unique,
PreliminarySourceIdentificationCategoryDisplayName varchar(500) not null constraint AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryDisplayName unique
)

Create table dbo.PreliminarySourceIdentificationType(
PreliminarySourceIdentificationTypeID int not null constraint PK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeID primary key,
PreliminarySourceIdentificationTypeName varchar(100) not null constraint AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeName unique,
PreliminarySourceIdentificationTypeDisplayName varchar(500) not null constraint AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeDisplayName unique,
PreliminarySourceIdentificationCategoryID int not null constraint FK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID foreign key references dbo.PreliminarySourceIdentificationCategory(PreliminarySourceIdentificationCategoryID)
)
go

Insert into dbo.PreliminarySourceIdentificationCategory (PreliminarySourceIdentificationCategoryID, PreliminarySourceIdentificationCategoryName, PreliminarySourceIdentificationCategoryDisplayName)
values
(1, 'Vehicles', 'Vehicles'),
(2, 'InadequateWasteContainerManagement', 'Inadequate Waste Container Management'),
(3, 'PedestrianLitter', 'Pedestrian Litter'),
(4, 'IllegalDumping', 'Illegal Dumping')

go

Insert into dbo.PreliminarySourceIdentificationType (PreliminarySourceIdentificationTypeID, PreliminarySourceIdentificationTypeDisplayName, PreliminarySourceIdentificationTypeName, PreliminarySourceIdentificationCategoryID)
values
(1,'Moving Vehicles', 'MovingVehicles', 1),
(2,'Parked Cars', 'ParkedCars', 1),
(3,'Uncovered Loads', 'UncoveredLoads', 1),
(4,'Vehicles (Other)', 'VehiclesOther', 1),
(5,'Overflowing or uncovered receptacles/dumpsters', 'OverflowingReceptacles', 2),
(6,'Dispersal of household trash and recyclables before, during, and after collection ', 'TrashDispersal', 2),
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
go

-- looooomg table mame with looooomg foreigm keys that had to be trumcated
Create table dbo.OnlandVisualTrashAssessmentPreliminarySourceIdentificationType (
OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID int not null identity(1,1) constraint PK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType primary key,
OnlandVisualTrashAssessmentID int not null constraint FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID foreign key references dbo.OnlandVisualTrashAssessment(OnlandVisualTrashAssessmentID),
PreliminarySourceIdentificationTypeID int not null constraint FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_PreliminarySourceIdentificationType_PreliminarySourceIdentific foreign key references dbo.PreliminarySourceIdentificationType(PreliminarySourceIdentificationTypeID),
ExplanationIfTypeIsOther varchar(500) null,
constraint CK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_ExplanationNotNullIfAndOnlyIfTypeIsOther
	-- big old XOR
	check ((ExplanationIfTypeIsOther is null or (PreliminarySourceIdentificationTypeID = 4 -- vehicles other
										     or PreliminarySourceIdentificationTypeID = 7 -- manglement other
										     or PreliminarySourceIdentificationTypeID = 13 -- pedestrians other
										     or PreliminarySourceIdentificationTypeID = 16)) -- dumping other

		   and not (ExplanationIfTypeIsOther is null and (PreliminarySourceIdentificationTypeID = 4 -- vehicles other
														    or PreliminarySourceIdentificationTypeID = 7 -- manglement other
													        or PreliminarySourceIdentificationTypeID = 13 -- pedestrians other
													        or PreliminarySourceIdentificationTypeID = 16)) -- dumping other											
		   ),
constraint AK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentID_PreliminarySourceIdentificationT unique(OnlandVisualTrashAssessmentID, PreliminarySourceIdentificationTypeID) -- at most one of each type selected per assessment
)