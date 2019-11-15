CREATE TABLE dbo.NetworkCatchmentStaging(
	NetworkCatchmentStagingID int IDENTITY(1,1) NOT NULL,
	DrainID varchar(10) NULL,
	Watershed varchar(100) NULL,
	CatchmentGeometry geometry NULL,
	OCSurveyCatchmentID int NULL,
	OCSurveyDownstreamCatchmentID int NULL
 CONSTRAINT PK_NetworkCatchmentStaging_NetworkCatchmentStagingID PRIMARY KEY CLUSTERED 
(
	NetworkCatchmentStagingID ASC
)
--,
-- CONSTRAINT AK_NetworkCatchmentStaging_OCSurveyCatchmentID UNIQUE NONCLUSTERED 
--(
--	OCSurveyCatchmentID ASC
--)
)
GO

--ALTER TABLE dbo.NetworkCatchmentStaging  WITH CHECK ADD  CONSTRAINT FK_NetworkCatchmentStaging_NetworkCatchmentStaging_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID FOREIGN KEY(OCSurveyDownstreamCatchmentID)
--REFERENCES dbo.NetworkCatchmentStaging (OCSurveyCatchmentID)
--GO

Insert into geometry_columns values
('Neptune', 'dbo', 'NetworkCatchmentStaging', 'CatchmentGeometry', 2, 2771, 'MULTIPOLYGON')

Alter table dbo.NetworkCatchment
Add LastUpdate datetime null
Go

Alter table dbo.NetworkCatchment
Alter column DrainID varchar(10) null

Alter table dbo.NetworkCatchment
Alter Column Watershed varchar(100) null