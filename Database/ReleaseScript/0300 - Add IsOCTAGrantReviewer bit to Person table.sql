alter table dbo.Person
add IsOCTAGrantReviewer bit null

go

update dbo.Person
set IsOCTAGrantReviewer = 0

update dbo.Person
set IsOCTAGrantReviewer = 1
where RoleID in (1, 4)

alter table dbo.Person
alter column IsOCTAGrantReviewer bit not null