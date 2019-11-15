-- duplicate NetworkCatchment for use by Drool Tool
CREATE TABLE dbo.Neighborhood(
	NeighborhoodID int IDENTITY(1,1) NOT NULL,
	DrainID varchar(10) NOT NULL,
	Watershed varchar(100) NOT NULL,
	NeighborhoodGeometry geometry NOT NULL,
	OCSurveyNeighborhoodID int NOT NULL,
	OCSurveyDownstreamNeighborhoodID int NULL,
	NeighborhoodGeometry4326 geometry NULL,
 CONSTRAINT PK_Neighborhood_NeighborhoodID PRIMARY KEY CLUSTERED 
(
	NeighborhoodID ASC
),
 CONSTRAINT AK_Neighborhood_OCSurveyNeighborhoodID UNIQUE NONCLUSTERED 
(
	OCSurveyNeighborhoodID ASC
)
)
GO

ALTER TABLE dbo.Neighborhood  WITH CHECK ADD  CONSTRAINT FK_Neighborhood_Neighborhood_OCSurveyDownstreamNeighborhoodID_OCSurveyNeighborhoodID FOREIGN KEY(OCSurveyDownstreamNeighborhoodID)
REFERENCES dbo.Neighborhood (OCSurveyNeighborhoodID)
GO

-- copy catchments
set identity_insert dbo.Neighborhood on
go
insert into dbo.Neighborhood (NeighborhoodID, DrainID, Watershed, NeighborhoodGeometry, OCSurveyNeighborhoodID, OCSurveyDownstreamNeighborhoodID, NeighborhoodGeometry4326)
Select 
	NetworkCatchmentID,
	DrainID,
	Watershed,
	CatchmentGeometry,
	OCSurveyCatchmentID,
	OCSurveyDownstreamCatchmentID,
	CatchmentGeometry4326
From dbo.NetworkCatchment
set identity_insert dbo.Neighborhood off

-- Copy the NetworkCatchmentID over to a NeighborhoodID
Alter table dbo.BackboneSegment
Add NeighborhoodID int null
GO

Update dbo.BackboneSegment
set NeighborhoodID = NetworkCatchmentID


ALTER TABLE dbo.BackboneSegment DROP CONSTRAINT FK_BackboneSegment_NetworkCatchment_NetworkCatchmentID
GO

ALTER TABLE dbo.BackboneSegment ADD CONSTRAINT FK_BackboneSegment_Neighborhood_NeighborhoodID FOREIGN KEY(NeighborhoodID)
REFERENCES dbo.Neighborhood (NeighborhoodID)
GO

Alter Table dbo.BackboneSegment
Drop Column NetworkCatchmentID

ALTER TABLE [dbo].[RawDroolMetric] DROP CONSTRAINT [FK_RawDroolMetric_NetworkCatchment_CatchIDN_OCSurveyCatchmentID]
GO

ALTER TABLE [dbo].[RawDroolMetric] Add CONSTRAINT [FK_RawDroolMetric_Neighborhood_CatchIDN_OCSurveyNeighborhoodID] FOREIGN KEY([MetricCatchIDN])
REFERENCES [dbo].Neighborhood ([OCSurveyNeighborhoodID])
GO