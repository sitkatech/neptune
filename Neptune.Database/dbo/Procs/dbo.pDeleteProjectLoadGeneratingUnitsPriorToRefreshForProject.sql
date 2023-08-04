Create Procedure dbo.pDeleteProjectLoadGeneratingUnitsPriorToRefreshForProject @ProjectID int

As
Delete from dbo.ProjectHRUCharacteristic where ProjectID = @ProjectID
Delete from dbo.ProjectLoadGeneratingUnit where ProjectID = @ProjectID
GO