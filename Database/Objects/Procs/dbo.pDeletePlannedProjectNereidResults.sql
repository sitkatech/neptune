IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeletePlannedProjectNereidResults')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeletePlannedProjectNereidResults
GO
Create Procedure dbo.pDeletePlannedProjectNereidResults (
@projectID int
)

As
Delete from dbo.PlannedProjectNereidResult where ProjectID = @projectID
GO