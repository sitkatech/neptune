create procedure dbo.pTrashGeneratingUnitMakeValid
as
begin

    update dbo.TrashGeneratingUnit set TrashGeneratingUnitGeometry = TrashGeneratingUnitGeometry.MakeValid() where TrashGeneratingUnitGeometry.STIsValid() = 0
end

GO