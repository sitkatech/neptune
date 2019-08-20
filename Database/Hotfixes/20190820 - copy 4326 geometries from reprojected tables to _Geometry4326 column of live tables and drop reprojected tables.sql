Update d
set d.DelineationGeometry4326 = dr.ogr_geometry
from dbo.Delineation d join dbo.DelineationReprojected dr on d.DelineationID = dr.DelineationID

Update s
set s.StormwaterJurisdictionGeometry4326 = sr.ogr_geometry
from dbo.StormwaterJurisdiction s join dbo.StormwaterJurisdictionReprojected sr on s.StormwaterJurisdictionID = sr.StormwaterJurisdictionID

update t
set t.LocationPoint4326 = tr.ogr_geometry
from dbo.TreatmentBMP t join dbo.TreatmentBMPReprojected tr on t.TreatmentBMPID = tr.TreatmentBMPID

update area
set area.OnlandVisualTrashAssessmentAreaGeometry4326 = arear.ogr_geometry
from dbo.OnlandVisualTrashAssessmentArea area join dbo.onlandvisualtrashassessmentareareprojected arear on area.OnlandVisualTrashAssessmentAreaID = arear.OnlandVisualTrashAssessmentAreaID

update l
set l.LandUseBlockGeometry4326 = lr.ogr_geometry
from dbo.LandUseBlock l join dbo.LandUseBlockReprojected lr on l.LandUseBlockID = lr.LandUseBlockID

--update p 
--set p.ParcelGeometry4326 = pr.ogr_geometry
--from parcel p join parcelreprojected pr on p.ParcelID = pr.ParcelID

Update d
set d.BackboneSegmentGeometry4326 = dr.ogr_geometry
from dbo.BackboneSegment d join dbo.BackboneSegmentReprojected dr on d.BackboneSegmentID = dr.BackboneSegmentID

Update d
set d.LocationPoint4326 = dr.ogr_geometry
from dbo.OnlandVisualTrashAssessmentObservation d join dbo.OnlandVisualTrashAssessmentObservationReprojected dr on d.OnlandVisualTrashAssessmentObservationID = dr.OnlandVisualTrashAssessmentObservationID

Update d
set d.TransectLine4326 = dr.ogr_geometry
from dbo.OnlandVisualTrashAssessmentArea d join dbo.TransectLineReprojected dr on d.OnlandVisualTrashAssessmentAreaID = dr.OnlandVisualTrashAssessmentAreaID

Update d
set d.WatershedGeometry4326 = dr.ogr_geometry
from dbo.Watershed d join dbo.WatershedReprojected dr on d.WatershedID = dr.WatershedID

update p 
set p.CatchmentGeometry4326 = pr.ogr_geometry
from NetworkCatchment p join NetworkCatchmentReprojected pr on p.NetworkCatchmentID = pr.NetworkCatchmentID

drop table if exists dbo.OnlandVisualTrashAssessmentAreaReprojected
drop table if exists dbo.DelineationReprojected
drop table if exists dbo.LandUseBlockReprojected
drop table if exists dbo.NetworkCatchmentReprojected
drop table if exists dbo.ParcelReprojected
drop table if exists dbo.StormwaterJurisdictionReprojected
drop table if exists dbo.TreatmentBMPReprojected
drop table if exists dbo.BackboneSegmentReprojected
drop table if exists dbo.OnlandVisualTrashAssessmentObservationReprojected
drop table if exists dbo.WatershedReprojected
drop table if exists dbo.TransectLineReprojected


