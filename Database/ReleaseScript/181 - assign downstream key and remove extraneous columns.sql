Alter Table dbo.BackboneSegment
Add DownstreamBackboneSegmentID int null
constraint FK_BackboneSegment_BackboneSegment_DownstreamBackboneSegmentID_BackboneSegmentID references dbo.BackboneSegment(BackboneSegmentID)
go

Update up
set up.DownstreamBackboneSegmentID = down.BackboneSegmentID
from dbo.BackboneSegment up left join dbo.BackboneSegment down on up.DownstreamBackboneSegmentAlternateID = down.BackboneSegmentAlternateID
go

Alter Table dbo.BackboneSegment
Drop Column DownstreamBackboneSegmentAlternateID

Alter Table dbo.BackboneSegment
Drop Constraint AK_BackboneSegment_BackboneSegmentAlternateID

Alter table dbo.BackboneSegment
Drop Column BackboneSegmentAlternateID
