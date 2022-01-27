insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(57, 'HippocampProjectInstructions', 'Hippocamp Project Instructions Page')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(57, '<p>Welcome to Hippocamp Project Instructions!</p> 
<p>Here are the steps to creating your project:</p> 
<ul>          
	<li>Fill out Project Basics</li>          
	<li>Complete Treatment BMPs Section</li>          
	<li>Mark out Delineations</li>
	<li>Review Modeled Performance</li>
	<li>Provide necessary Attachments</li>
	<li>Review Project</li>
	<li>Submit</li>
</ul>')