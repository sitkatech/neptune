drop procedure if exists dbo.pLandUseBlockStagingDeleteByPersonID
GO

Create Procedure dbo.pLandUseBlockStagingDeleteByPersonID
(
	@personID int
)
As

delete from dbo.LandUseBlockStaging where UploadedByPersonID = @personID

GO