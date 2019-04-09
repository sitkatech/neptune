DROP VIEW IF EXISTS dbo.vOnlandVisualTrashAssessmentAreaDated
GO

Create View dbo.vOnlandVisualTrashAssessmentAreaDated as
Select a.OnlandVisualTrashAssessmentAreaID, a.OnlandVisualTrashAssessmentAreaGeometry, q.MostRecentAssessmentDate from OnlandVisualTrashAssessmentArea a inner join 
(Select OnlandVisualTrashAssessmentAreaID, Max(CompletedDate) as MostRecentAssessmentDate From OnlandVisualTrashAssessment
where OnlandVisualTrashAssessmentStatusID = 2
group by OnlandVisualTrashAssessmentAreaID) q on a.OnlandVisualTrashAssessmentAreaID = q.OnlandVisualTrashAssessmentAreaID
go