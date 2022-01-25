delete from dbo.ProjectStatus
go

insert into dbo.ProjectStatus(ProjectStatusID, ProjectStatusName, ProjectStatusDisplayName, ProjectStatusSortOrder)
values
(1, 'Draft', 'Draft', 10)