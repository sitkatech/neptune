alter table dbo.FundingEventType add SortOrder int null

go 
update dbo.FundingEventType set SortOrder = FundingEventTypeID

go

alter table dbo.FundingEventType alter column SortOrder int not null

