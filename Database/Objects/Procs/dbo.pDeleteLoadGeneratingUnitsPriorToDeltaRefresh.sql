IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh
GO

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


IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh
GO

Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh 
As
Delete from dbo.HRUCharacteristic
Delete from dbo.LoadGeneratingUnit
GO