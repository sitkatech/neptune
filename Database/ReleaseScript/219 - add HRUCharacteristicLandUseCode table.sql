create table dbo.HRUCharacteristicLandUseCode
(
	HRUCharacteristicLandUseCodeID int not null constraint PK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID primary key,
	HRUCharacteristicLandUseCodeName varchar(100) not null constraint AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeName unique,
	HRUCharacteristicLandUseCodeDisplayName varchar(100) not null constraint AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeDisplayName unique
)

alter table dbo.HRUCharacteristic add HRUCharacteristicLandUseCodeID int null constraint FK_HRUCharacteristic_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeID foreign key references dbo.HRUCharacteristicLandUseCode(HRUCharacteristicLandUseCodeID)
GO


insert into dbo.HRUCharacteristicLandUseCode (HRUCharacteristicLandUseCodeID, HRUCharacteristicLandUseCodeName, HRUCharacteristicLandUseCodeDisplayName)
values
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


update hru set hru.HRUCharacteristicLandUseCodeID = hrulc.HRUCharacteristicLandUseCodeID
from dbo.HRUCharacteristic hru
join dbo.HRUCharacteristicLandUseCode hrulc on hru.LSPCLandUseDescription = hrulc.HRUCharacteristicLandUseCodeName

alter table dbo.HRUCharacteristic alter column HRUCharacteristicLandUseCodeID int not null


alter table dbo.HRUCharacteristic drop column LSPCLandUseDescription