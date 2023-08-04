MERGE INTO dbo.ProjectNetworkSolveHistoryStatusType AS Target
USING (VALUES
(1, 'Queued', 'Queued'),
(2, 'Succeeded', 'Succeeded'),
(3, 'Failed', 'Failed')
)
AS Source (ProjectNetworkSolveHistoryStatusTypeID, ProjectNetworkSolveHistoryStatusTypeName, ProjectNetworkSolveHistoryStatusTypeDisplayName)
ON Target.ProjectNetworkSolveHistoryStatusTypeID = Source.ProjectNetworkSolveHistoryStatusTypeID
WHEN MATCHED THEN
UPDATE SET
	ProjectNetworkSolveHistoryStatusTypeName = Source.ProjectNetworkSolveHistoryStatusTypeName,
	ProjectNetworkSolveHistoryStatusTypeDisplayName = Source.ProjectNetworkSolveHistoryStatusTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ProjectNetworkSolveHistoryStatusTypeID, ProjectNetworkSolveHistoryStatusTypeName, ProjectNetworkSolveHistoryStatusTypeDisplayName)
	VALUES (ProjectNetworkSolveHistoryStatusTypeID, ProjectNetworkSolveHistoryStatusTypeName, ProjectNetworkSolveHistoryStatusTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;