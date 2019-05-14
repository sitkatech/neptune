
insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(39, 'DroolToolHomePage', 'Drool Tool Home Page', 2),
(40, 'DroolToolAboutPage', 'Drool Tool About Page', 2)

insert into dbo.NeptunePage(NeptunePageTypeID)
values
(39),
(40)
