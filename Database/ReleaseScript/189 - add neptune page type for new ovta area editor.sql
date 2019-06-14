insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(42, 'EditOVTAArea', 'Edit OVTA Area', 2)
go

Insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(42, N'<p>You may click or tap the edit button on the map below to adjust the Assessment Area&#39;s vertices. You may drag vertices to move them, or click or tap vertices to delete them. Note that it may be necessary to zoom the map to see individual vertices that are close together (e.g. there may be many closely-placed vertices along curves). You may click the &quot;Reset Map Zoom&quot; button to zoom back out to the entire Assessment Area.</p>  <p>Note that the Assessment Area&#39;s edges should not intersect each other. If the area contains any edges that intersect each other, they will be split into separate shapes as necessary.</p>  <p>This will affect all Assessments associated with this Assessment Area.</p>')
