create procedure dbo.pTrashGeneratingUnit4326MakeValid
as
begin

    update dbo.TrashGeneratingUnit4326 set TrashGeneratingUnit4326Geometry = TrashGeneratingUnit4326Geometry.MakeValid() where TrashGeneratingUnit4326Geometry.STIsValid() = 0
end

GO