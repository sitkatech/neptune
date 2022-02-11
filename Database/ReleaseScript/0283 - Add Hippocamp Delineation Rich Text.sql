insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(61, 'HippocampDelineations', 'Hippocamp Delineations Page')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(61, 
	'<p>
		Delineate the drainage area for your proposed BMPs in order to prepare them for model calculations. 
		You can toggle on the Existing Delineations layer to view existing assets and make sure your project 
		does not overlap. 
		<ul>
			<li>A distributed BMP only receives surface flow and can be delineated by drawing on the map or uploading a GIS File</li>
			<li>A centralized BMP receives piped flow in addition to surface flow. The tool allows you to trace the Regional Subbasin Network to identify the drainage area. If the trace results in an area larger than expected, you can request revisions to the RSB catchments by emailing <a href="mailto:Justin.Grewal@ocpw.ocgov.com">Justin.Grewal@ocpw.ocgov.com</a></li>
		</ul>
	</p>'
)