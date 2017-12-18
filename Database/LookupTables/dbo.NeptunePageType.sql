delete from dbo.NeptunePageType
go

insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(1, 'HomePage', 'Home Page', 2),
(2, 'About', 'About', 2),
(3, 'OrganizationsList', 'Organizations List', 1),
(4, 'HomeMapInfo', 'Home Page Map Info', 2),
(5, 'HomeAdditionalInfo', 'Home Page Additional Info', 2)