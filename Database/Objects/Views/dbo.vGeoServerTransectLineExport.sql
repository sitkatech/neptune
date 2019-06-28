Drop View If Exists dbo.vGeoServerTransectLineExport
Go

Create View dbo.vGeoServerTransectLineExport as
Select OnlandVisualTrashAssessmentAreaName as OVTAAreaName, TransectLine, StormwaterJurisdictionID as JurisID from dbo.OnlandVisualTrashAssessmentArea
Go
