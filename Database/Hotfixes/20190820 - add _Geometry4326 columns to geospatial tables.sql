Alter table dbo.Delineation
Add DelineationGeometry4326 Geometry null

Alter table dbo.StormwaterJurisdiction
Add StormwaterJurisdictionGeometry4326 Geometry null

Alter table dbo.TreatmentBMP
Add LocationPoint4326 Geometry null

Alter Table dbo.OnlandVisualTrashAssessmentArea
Add OnlandVisualTrashAssessmentAreaGeometry4326 geometry null

Alter table dbo.LandUseBlock
Add LandUseBlock4326 geometry null

Alter Table dbo.Parcel
Add ParcelGeometry4326 geometry null

Alter table dbo.BackboneSegment
Add BackboneSegmentGeometry4326 geometry null

Alter table dbo.OnlandVisualTrashAssessmentObservation
Add LocationPoint4326 geometry null

Alter table dbo.OnlandVisualTrashAssessmentArea
Add TransectLine4326 geometry null

Alter Table dbo.Watershed
Add WatershedGeometry4326 geometry null

Alter Table dbo.NetworkCatchment
Add NetworkCatchment4326 geometry null