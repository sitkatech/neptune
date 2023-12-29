Create Procedure dbo.pParcelStagingDelete
with execute as owner
As

truncate table dbo.ParcelStaging

GO