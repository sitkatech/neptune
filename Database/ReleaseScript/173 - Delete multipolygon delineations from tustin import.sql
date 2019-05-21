update t
set DelineationID = null 
from dbo.TreatmentBMP t inner join dbo.Delineation d on t.DelineationID = d.DelineationID
where d.DelineationGeometry.STGeometryType() = 'MULTIPOLYGON'

delete from dbo.Delineation where DelineationGeometry.STGeometryType() = 'MULTIPOLYGON'

select * from Neptune.dbo.Delineation where VerifiedByPersonID = 1119