CREATE TABLE dbo.NeptuneArea(
	NeptuneAreaID int NOT NULL,
	NeptuneAreaName varchar(20) NOT NULL,
	NeptuneAreaDisplayName varchar(40) NOT NULL,
	SortOrder int NOT NULL,
	ShowOnPrimaryNavigation bit NOT NULL,
 CONSTRAINT PK_NeptuneArea_NeptuneAreaID PRIMARY KEY CLUSTERED 
(
	NeptuneAreaID ASC
),
 CONSTRAINT AK_NeptuneArea_NeptuneAreaDisplayName UNIQUE NONCLUSTERED 
(
	NeptuneAreaDisplayName ASC
),
 CONSTRAINT AK_NeptuneArea_NeptuneAreaName UNIQUE NONCLUSTERED 
(
	NeptuneAreaName ASC
)
)
