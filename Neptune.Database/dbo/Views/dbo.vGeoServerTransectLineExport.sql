Create View dbo.vGeoServerTransectLineExport as
Select OnlandVisualTrashAssessmentAreaName as OVTAAreaName, TransectLine, StormwaterJurisdictionID as JurisID from dbo.OnlandVisualTrashAssessmentArea where TransectLine is not null
Go
