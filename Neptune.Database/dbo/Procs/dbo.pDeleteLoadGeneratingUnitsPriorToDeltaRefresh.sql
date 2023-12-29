Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID int
As

Declare @LoadGeneratingUnitRefreshAreaGeometry geometry;
Select @LoadGeneratingUnitRefreshAreaGeometry = LoadGeneratingUnitRefreshAreaGeometry from dbo.LoadGeneratingUnitRefreshArea where LoadGeneratingUnitRefreshAreaID = @LoadGeneratingUnitRefreshAreaID

-- delete affected HRUs
delete hru from dbo.LoadGeneratingUnit lgu join dbo.HRUCharacteristic hru on lgu.LoadGeneratingUnitID = hru.LoadGeneratingUnitID
where LoadGeneratingUnitGeometry.STIntersects(@LoadGeneratingUnitRefreshAreaGeometry) = 1

-- delete affected LGUs
delete from dbo.LoadGeneratingUnit where LoadGeneratingUnitGeometry.STIntersects(@LoadGeneratingUnitRefreshAreaGeometry) = 1

GO