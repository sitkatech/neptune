alter table dbo.Delineation
add DateLastModified DateTime null

go

update dbo.Delineation
set DateLastModified = GETDATE()

go

alter table dbo.Delineation
alter column DateLastModified DateTime not null
