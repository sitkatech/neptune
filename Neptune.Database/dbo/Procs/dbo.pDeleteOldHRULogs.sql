Create Procedure dbo.pDeleteOldHRULogs
with execute as owner
As

delete from dbo.HRULog
where HRULogID not in
(
    select hrul.HRULogID
    from dbo.HRULog hrul
    join dbo.LoadGeneratingUnit lgu on hrul.HRULogID = lgu.HRULogID

    union

    select hrul.HRULogID
    from dbo.HRULog hrul
    join dbo.ProjectLoadGeneratingUnit plgu on hrul.HRULogID = plgu.HRULogID
)

GO