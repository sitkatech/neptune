-- TODO: Both of these procs will need to be updated to delete corresponding HRU characteristcs prior to the LGU delete

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteLoadGeneratingUnitsPriorToDeltaInsert')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteLoadGeneratingUnitsPriorToDeltaInsert
GO

Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToDeltaRefresh @LoadGeneratingUnitRefreshAreaID int
As

Declare @LoadGeneratingUnitRefreshAreaGeometry geometry;
Select @LoadGeneratingUnitRefreshAreaGeometry = LoadGeneratingUnitRefreshAreaGeometry from dbo.LoadGeneratingUnitRefreshArea where LoadGeneratingUnitRefreshAreaID = @LoadGeneratingUnitRefreshAreaID

Delete from dbo.LoadGeneratingUnit where LoadGeneratingUnitGeometry.STIntersects(@LoadGeneratingUnitRefreshAreaGeometry) = 1

GO


IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteLoadGeneratingUnitsPriorToDeltaInsert
GO

Create Procedure dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh 
As

Delete from dbo.LoadGeneratingUnit
GO