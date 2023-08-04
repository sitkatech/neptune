Create Procedure dbo.pDeleteProjectNereidResults (
@projectID int
)

As
Delete from dbo.ProjectNereidResult where ProjectID = @projectID
GO