insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(55, 'HippocampAbout', 'Hippocamp About Page')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(55, '<p>Welcome to Hippocamp.  Some fun facts:</p>      
<ul>          
	<li>Hippocampi, or hippokampos, are mythical creatures of the sea. They are half horse and half fish.</li>          
	<li>The name Hippocampus is derived from the Greek hippocamp (hippos, meaning "horse," and kampos, meaning "sea monster"), since the structure''s shape resembles that of a sea horse.</li>          
	<li>It is a small moon of Neptune discovered on July 1, 2013. It was found by astronomer Mark Showalter by analyzing archived Neptune photographs the Hubble Space Telescope captured between 2004 and 2009.</li>
	<li>Has the designation, Neptune XIV.</li>
	<li>It is also the codename for the OCST Planning module.</li>
</ul>')