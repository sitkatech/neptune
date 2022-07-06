insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values
(68, 'UploadWQMPs', 'Bulk Upload Water Quality Management Plans')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values
(68, '<p>Use this form to bulk upload WQMPs or bulk update existing WQMPs in the system. If the upload fails a detailed error report will be provided, otherwise WQMPs will be imported per the CSV. Once uploaded, this action cannot be undone. <strong>It is strongly advised to test the upload in the QA site (<a href="https://qa.ocstormwatertools.org">https://qa.ocstormwatertools.org</a>) prior to bulk uploading data to the Production site.</strong></p>
<p>The CSV must have a header row with column names that match the label of the field to be uploaded.</p>
<p>All Basics field names must exist in the CSV header, but can have blank/null data in the CSV. Some fields are optional and others are required when adding new WQMPs to the system. For a bulk update, required Basic fields (E.g Trash Capture Status and Sizing Basis) must be in the CSV header, but values are not necessary in the body of the CSV, unless new values are provided for update.</p>
<p><strong>The CSV should follow this format:</strong></p> <p><table align="left" border="1">  <tbody>   <tr>    <td><strong>Required Properties</strong></td>   </tr>   <tr>    <td> WQMP Name, Jurisdiction, Land Use, Priority, Status, Development Type, Trash Capture Status</td>   </tr>  <tr>    <td><strong>Optional Properties</strong></td> </tr> <tr> <td>Maintenance Contact Name, Maintenance Contact Organization, Maintenance Contact Phone, Maintenance Contact Address 1, Maintenance Contact Address 2, Maintenance Contact City, Maintenance Contact State, Maintenance Contact Zip, Permit Term, Hydromodification Controls Apply, Approval Date, Date of Construction, Hydrologic Subarea, Record Number, Recorded WQMP Area (Acres)</td>   </tr> </tbody> </table> </p> <p>&nbsp;</p>  <hr /> <p>&nbsp;</p> 
')



