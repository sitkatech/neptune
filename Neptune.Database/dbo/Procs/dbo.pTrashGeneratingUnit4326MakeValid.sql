create procedure dbo.pTrashGeneratingUnit4326MakeValid
as
begin

    update dbo.TrashGeneratingUnit4326 set TrashGeneratingUnit4326Geometry = TrashGeneratingUnit4326Geometry.MakeValid() where TrashGeneratingUnit4326Geometry.STIsValid() = 0
    -- buffering by a very small number to get rid of GeometryCollection
    update dbo.TrashGeneratingUnit4326 set TrashGeneratingUnit4326Geometry = TrashGeneratingUnit4326Geometry.STBuffer(.000000000001) where TrashGeneratingUnit4326Geometry.STGeometryType() = 'GeometryCollection'
end

GO