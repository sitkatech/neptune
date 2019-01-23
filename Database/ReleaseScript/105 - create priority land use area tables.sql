Create Table PriorityLandUseType(
PriorityLandUseTypeID int not null constraint PK_PriorityLandUseType_PriorityLandUseTypeID primary key,
PriorityLandUseTypeName varchar(100) not null constraint AK_PriorityLandUseType_PriorityLandUseTypeName unique,
PriorityLandUseTypeDisplayName varchar(100) not null constraint AK_PriorityLandUseType_PriorityLandUseTypeDisplayName unique,
MapColorHexCode varchar(7) not null
)
Go

insert into PriorityLandUseType (PriorityLandUseTypeID,PriorityLandUseTypeName,PriorityLandUseTypeDisplayName, MapColorHexCode)
values
(1,'Commercial','Commercial', '#c2fbfc'),
(2,'HighDensity','High Density','#c0d6fc'),
(3,'Industrial','Industrial','#b4fcb3'),
(4,'MixedUrban','Mixed Urban','#fcb6b9'),
(5,'Retail','Retail','#f2cafc'),
(6,'Transportation','Transportation','#fcd6b6')

Create Table LandUseBlock(
LandUseBlockID int not null identity(1,1) constraint PK_LandUseBlock_LandUseBlockID primary key,
PriorityLandUseTypeID int null constraint FK_LandUseBlock_PriorityLandUseType_PriorityLandUseTypeID
	foreign key references dbo.PriorityLandUseType(PriorityLandUseTypeID),
LandUseDescription varchar(500) null,
LandUseBlockGeometry geometry not null
)
Go