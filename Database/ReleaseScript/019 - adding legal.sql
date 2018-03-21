insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
values
(20, 'Legal', 'Legal', 2)

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent, TenantID)
values
(20, '<p>
	The Stormwater Tools application is built by <a href="http://www.sitkatech.com/">Sitka Technology Group</a> in partnership with <a href="https://www.geosyntec.com/">Geosyntec Consultants</a> for <a href="http://www.ocpublicworks.com/">Orange County Public Works</a>.
	The Stormwater Tools application is derived from the <a href="http://www.trpa.org/">Tahoe Regional Planning Agency</a>''s Lake Tahoe Info Stormwater Tools.
</p>
<p>
	This program is free software; you can redistribute it and/or modify it under the terms of the <a href="https://www.gnu.org/licenses/agpl-3.0.en.html">GNU Affero General Public License</a> as published by the Free Software Foundation.
	Source code is available on <a href="http://github.com/sitkatech/neptune/">GitHub</a>.
</p>
<p>
	Copyright &copy; <a href="http://www.trpa.org/">Tahoe Regional Planning Agency</a> and <a href="http://www.sitkatech.com/">Sitka Technology Group</a>
</p>
', 2)
