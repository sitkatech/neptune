IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject
GO

Create Procedure dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID int

As
Delete from dbo.ProjectHRUCharacteristic where ProjectID = @ProjectID
Delete from dbo.ProjectLoadGeneratingUnit where ProjectID = @ProjectID
GO