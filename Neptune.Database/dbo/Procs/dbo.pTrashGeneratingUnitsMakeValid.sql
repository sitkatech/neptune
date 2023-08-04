Create Procedure dbo.pTrashGeneratingUnitsMakeValid

As

update dbo.TrashGeneratingUnit
set TrashGeneratingUnitGeometry = TrashGeneratingUnitGeometry.MakeValid() 
where TrashGeneratingUnitGeometry.STIsValid() = 0

update dbo.TrashGeneratingUnit4326
set TrashGeneratingUnit4326Geometry = TrashGeneratingUnit4326Geometry.MakeValid() 
where TrashGeneratingUnit4326Geometry.STIsValid() = 0

GO