drop procedure if exists dbo.pParcelStagingDeleteByPersonID
GO

Create Procedure dbo.pParcelStagingDeleteByPersonID
(
	@personID int
)
As

truncate table dbo.ParcelStaging

GO