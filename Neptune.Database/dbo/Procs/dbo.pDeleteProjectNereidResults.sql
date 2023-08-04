IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteProjectNereidResults')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteProjectNereidResults
GO
Create Procedure dbo.pDeleteProjectNereidResults (
@projectID int
)

As
Delete from dbo.ProjectNereidResult where ProjectID = @projectID
GO