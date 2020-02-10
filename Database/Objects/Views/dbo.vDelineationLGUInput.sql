Drop View If Exists dbo.vDelineationLGUInput
GO

Create view dbo.vDelineationLGUInput
as
Select
	DelineationID,
	DelineationGeometry
from dbo.Delineation
Where DelineationTypeID = 2
GO