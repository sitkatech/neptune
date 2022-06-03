insert into dbo.NeptunePageType(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName)
values 
	(65, 'OCTAM2Tier2GrantProgramMetrics', 'OCTA M2 Tier 2 Grant Program Metrics'),
	(66, 'OCTAM2Tier2GrantProgramDashboard', 'OCTA M2 Tier 2 Grant Program Dashboard')

insert into dbo.NeptunePage(NeptunePageTypeID, NeptunePageContent)
values 
	(65, 'Here is a place to display any disclaimer/caveat information regarding OCTA M2 Tier 2 Grant Program metrics'),
	(66, 'A list of all projects that have been shared with the OCTA M2 Tier 2 Grant Program.')