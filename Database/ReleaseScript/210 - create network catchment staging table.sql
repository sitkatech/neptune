CREATE TABLE dbo.NetworkCatchmentStaging(
	NetworkCatchmentStagingID int IDENTITY(1,1) NOT NULL,
	DrainID varchar(10) NOT NULL,
	Watershed varchar(100) NOT NULL,
	CatchmentGeometry geometry NOT NULL,
	OCSurveyCatchmentID int NOT NULL,
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