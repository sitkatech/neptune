Create Table dbo.BackboneSegmentType(
BackboneSegmentTypeID Int not null constraint PK_BackboneSegmentType_BackboneSegmentTypeID primary key,
BackboneSegmentTypeName varchar(20) not null constraint AK_BackboneSegmentType_BackboneSegmentTypeName unique,
BackboneSegmentTypeDisplayName varchar(20) not null constraint AK_BackboneSegmentType_BackboneSegmentTypeDisplayName unique
)
Go

Insert into dbo.BackboneSegmentType (BackboneSegmentTypeID, BackboneSegmentTypeName, BackboneSegmentTypeDisplayName)
values 
(1, 'Dummy', 'Dummy'),
(2,'StormDrain', 'Storm Drain'),
(3, 'Channel', 'Channel')

go

Create Table dbo.BackboneSegment(
BackboneSegmentID int not null identity(1,1) constraint PK_BackboneSegment_BackboneSegmentID primary key,
BackboneSegmentGeometry geometry not null,
BackboneSegmentAlternateID varchar(10) not null constraint AK_BackboneSegment_BackboneSegmentAlternateID unique,
DownstreamBackboneSegmentID varchar(10) null,
CatchIDN int not null,
NetworkCatchmentID int null constraint FK_BackboneSegment_NetworkCatchment_NetworkCatchmentID foreign key references dbo.NetworkCatchment(NetworkCatchmentID),
BackboneSegmentTypeID int not null constraint FK_BackboneSegment_BackboneSegmentType_BackboneSegmentTypeID foreign key references dbo.BackboneSegmentType(BackboneSegmentTypeID)
)
Go	