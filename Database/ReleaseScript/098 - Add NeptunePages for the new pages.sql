
insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(32, 'ModelingHomePage', 'Modeling Home Page', 2),
(33, 'TrashHomePage', 'Trash Home Page', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(32, Null),
(33, Null)