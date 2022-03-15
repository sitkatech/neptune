IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeletePlannedProjectLoadGeneratingUnitsPriorToRefreshForProject')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeletePlannedProjectLoadGeneratingUnitsPriorToRefreshForProject
GO

Create Procedure dbo.pDeletePlannedProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID int

As
Delete from dbo.PlannedProjectHRUCharacteristic where ProjectID = @ProjectID
Delete from dbo.PlannedProjectLoadGeneratingUnit where ProjectID = @ProjectID
GO