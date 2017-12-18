delete from dbo.NeptunePageRenderType
go

insert into dbo.NeptunePageRenderType(NeptunePageRenderTypeID, NeptunePageRenderTypeName, NeptunePageRenderTypeDisplayName)
values
(1, 'IntroductoryText', 'Introductory Text'),
(2, 'PageContent', 'Page Content')