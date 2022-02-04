insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(60, 'HippocampTreatmentBMPs', 'Hippocamp Treatment BMPs Page')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(60, 
	'<p>
		On this page, you can add proposed Treatment BMPs to your project. Once a proposed BMP is created, 
		it will display on the map. To view details, edit, or delete a proposed BMP, either select it on 
		the map or from the list to its left.
	</p>'
)