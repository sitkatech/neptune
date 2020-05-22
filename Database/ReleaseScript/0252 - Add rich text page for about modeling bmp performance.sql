insert into dbo.NeptunePageType
	(NeptunePageTypeID, NeptunePageTypeName, NeptunePageTypeDisplayName, NeptunePageRenderTypeID)
VALUES
	(50, 'AboutModelingBMPPerformance', 'About Modeling BMP Performance', 2)

Insert into dbo.NeptunePage (NeptunePageTypeID)
VALUES
	(50)