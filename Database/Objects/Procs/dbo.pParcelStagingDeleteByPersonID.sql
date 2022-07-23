drop procedure if exists dbo.pParcelStagingDeleteByPersonID
GO

Create Procedure dbo.pParcelStagingDeleteByPersonID
(
	@personID int
)
As

delete from dbo.ParcelStaging where UploadedByPersonID = @personID

GO