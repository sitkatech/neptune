Create Table dbo.NetworkCatchment(
NetworkCatchmentID int not null identity (1,1) constraint PK_NetworkCatchment_NetworkCatchmentID primary key,
OCSurveyCatchmentID varchar(10) not null,
DownstreamCatchmentID varchar(10) not null,
DrainID varchar(10) not null,
Watershed varchar(100) not null, -- todo: normalize by pulling out a 'something something Watershed' table
CatchmentGeometry geometry not null
constraint AK_NetworkCatchment_OCSurveyCatchmentID_DrainID unique (OCSurveyCatchmentID, DrainID)
)