insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values (49, 'UploadTreatmentBMPs', 'Upload Treatment BMPs', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values (49, '<p>Use this form to bulk upload Treatment BMPs. If the upload fails a 
			detailed error report will be provided, otherwise BMPs will be imported per 
			the CSV. Once uploaded, this action cannot be undone. <strong>It is strongly 
			advised to test the upload in the QA environment prior to bulk uploading data 
			to Production.</strong> The CSV must have a header row with column names that 
			match the label of the field to be uploaded. The CSV should follow this 
			general format:</p>
			<pre class="panel">
			BMP Name,Latitude,Longitude,Jurisdiction,Owner,Year Built or Installed,Asset ID in System of Record,Required Lifespan of Installation,Allowable End Date of Installation (if applicable),Required Field Visits Per Year,Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
			Test2,30,10,City Of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,Not Provided
			</pre>
			<p>Some fields are optional and others are required. Optional fields must 
			exist in the CSV header, but can have blank/null data in the CSV. The fields 
			must appear in the specified order.</p>')
