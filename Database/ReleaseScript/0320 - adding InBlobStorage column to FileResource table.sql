-- begin tran

alter table dbo.FileResource add
[InBlobStorage] [bit] NOT NULL default(0);

-- rollback tran