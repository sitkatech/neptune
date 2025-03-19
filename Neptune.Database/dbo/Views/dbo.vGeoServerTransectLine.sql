Create View dbo.vGeoServerTransectLine
as

Select OnlandVisualTrashAssessmentAreaID, OnlandVisualTrashAssessmentAreaName, TransectLine, StormwaterJurisdictionID
from dbo.OnlandVisualTrashAssessmentArea 
where TransectLine is not null

Go
