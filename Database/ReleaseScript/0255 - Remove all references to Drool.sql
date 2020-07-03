alter table dbo.Person drop constraint FK_Person_DroolToolRole_DroolToolRoleID
alter table dbo.Person drop column DroolToolRoleID

drop table dbo.DroolToolWatershed
drop table dbo.DroolToolRole
drop table dbo.RawDroolMetric
drop table dbo.BackboneSegment
drop table dbo.BackboneSegmentType
drop table dbo.Neighborhood

delete from dbo.NeptuneArea where NeptuneAreaName = 'DroolTool'
delete from dbo.NeptunePage where NeptunePageTypeID in (select NeptunePageTypeID from dbo.NeptunePageType where NeptunePageTypeName = 'DroolToolAboutPage' or NeptunePageTypeName = 'DroolToolHomePage')
delete from dbo.NeptunePageType where NeptunePageTypeName in ('DroolToolHomePage', 'DroolToolAboutPage')
